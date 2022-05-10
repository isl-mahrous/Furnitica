import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IBasket, IBasketItem, IBasketTotals } from '../shared/models/basket';
import { IProduct } from '../shared/models/product';
import { productViewModel } from '../shared/viewmodels/product-viewmodel';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<IBasket>(null);
  private basketTotalSource = new BehaviorSubject<IBasketTotals>(null);
  basket$ = this.basketSource.asObservable(); 
  basketTotal$ = this.basketTotalSource.asObservable();
  userId = "114ea7f4-b4cb-4614-a50f-e23ab17101ef"

  constructor(private http:HttpClient) {
    this.getBasket(this.userId);
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
  deleteBasket(basket: IBasket) {
    return this.http.delete(`${this.baseUrl}basket?id=${basket.id}`).subscribe(() => {
      this.basketSource.next(null);
      this.basketTotalSource.next(null);
    });
  }

  private calculateTotals() {
    const basket = this.getCurrentBasketValue();
    // TODO : Change this later
    const shipping = 0;
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
 
  
}
