import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, delay, map, Observable, switchMap, timer } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AccountService } from '../account/account.service';
import { IBasket, IBasketItem, IBasketTotals } from '../shared/models/basket';
import { IDeliveryMethod } from '../shared/models/deliveryMethod';
import { IProduct } from '../shared/models/product';
import { productViewModel } from '../shared/viewmodels/product-viewmodel';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  private baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<IBasket>(null);
  private basketTotalSource = new BehaviorSubject<IBasketTotals>(null);

  private userId = "d54b4448-50e0-47d1-a3a3-c4d07610e759"

  basket$ = this.basketSource.asObservable();
  basketTotal$ = this.basketTotalSource.asObservable();
  shipping = 0;

  constructor(private http:HttpClient, private accountService : AccountService) {
    timer(1000).pipe(
      delay(1000),
      switchMap(() => this.getUserId()),
    ).subscribe((userId) => {
      this.getBasket(userId);
    })
  }

  getBasket(userId:string){
    return this.http.get<IBasket>(`${this.baseUrl}basket?userId=${userId}`)
    .subscribe(
      (basket) => {
        this.basketSource.next(basket);
        this.calculateTotals();
      }
    )
  }

  setBasket(basket: IBasket)
  {
    return this.http.post<IBasket>(`${this.baseUrl}basket`, basket)
    .subscribe(
      (response) => {
      this.basketSource.next(response);
      this.calculateTotals();
    })
  }

  getCurrentBasketValue()
  {
    return this.basketSource.value;
  }

  addItemToBasket(product: IProduct, quantity = 1)
  {
    const itemToAdd: IBasketItem = {
      id : 0,
      price : product.price,
      quantity,
      productId : product.id,
      basketId : (this.getCurrentBasketValue().id)
    }
    let basket = this.getCurrentBasketValue();
    basket.basketItems = this.addOrUpdateItem(basket.basketItems, itemToAdd, quantity);
    this.setBasket(basket);
  }

  incrementItemQuantity (product : productViewModel){
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.basketItems.findIndex(i => i.productId === product.id);
    basket.basketItems[foundItemIndex].quantity++;
    this.setBasket(basket);
  }

  decrementItemQuantity (product : productViewModel){
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.basketItems.findIndex(i => i.productId === product.id);
    if (basket.basketItems[foundItemIndex].quantity > 1){
      basket.basketItems[foundItemIndex].quantity--;
      this.setBasket(basket);
    }
    else{
      this.removeItemFromBasket(product);
    }
  }

  removeItemFromBasket(product : productViewModel) {
    let basket = this.getCurrentBasketValue();
    const item = basket.basketItems.find(i => i.productId === product.id);
    if (basket.basketItems.some(i => i.productId === item.productId)){
      basket.basketItems = basket.basketItems.filter(i => i.productId !== item.productId);
    }
    if (basket.basketItems.length>0){
      this.setBasket(basket);
    }
    else{
      this.deleteBasket(basket);
    }

  }

  deleteLocalBasket(id : number) {
    this.basketSource.next(null);
    this.basketTotalSource.next(null);
    localStorage.removeItem('basket_id');
  }

  deleteBasket(basket: IBasket) {
    return this.http.delete(`${this.baseUrl}basket?id=${basket.id}`).subscribe(() => {
      this.basketSource.next(null);
      this.basketTotalSource.next(null);
    });
  }

  private calculateTotals() {
    const basket = this.getCurrentBasketValue();
    // TODO : Change this later
    const shipping = this.shipping;
    const subtotal = basket.basketItems.reduce((a,b) => (b.price * b.quantity) + a, 0);
    const total = subtotal + shipping;
    this.basketTotalSource.next({
      shipping, total, subtotal
    })
  }

  private addOrUpdateItem(basketItems: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    if (!basketItems)
      basketItems = [];
    const index = basketItems.findIndex(i => i.productId === itemToAdd.productId);
    if (index === -1){
      itemToAdd.quantity = quantity;
      basketItems.push(itemToAdd);
    }
    else{
      basketItems[index].quantity += quantity;
    }
    return basketItems;
  }

  getBasketProducts(basketId:number)
  {
    return this.http.get<IProduct[]>(`${this.baseUrl}basket/${basketId}`);
  }

  setShippingPrice(deliveryMethod: IDeliveryMethod) {
    this.shipping = deliveryMethod.price;
    this.calculateTotals();
  }

  private getUserId(){
    const user = this.accountService.getCurrentUserValue();
    return new Observable <string>(observer => {
      observer.next(user.userId);
      observer.complete();
    })
  }

}
