import { Component, OnInit } from '@angular/core';
import { delay, Observable, switchMap, timer } from 'rxjs';
import { IBasket, IBasketItem } from '../shared/models/basket';
import { IProduct } from '../shared/models/product';
import { productViewModel } from '../shared/viewmodels/product-viewmodel';
import { BasketService } from './basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {
  basket$: Observable<IBasket>;
  basketProducts: productViewModel[] = [];

  constructor(private basketService: BasketService) {
    this.basket$ = this.basketService.basket$;
   };

  ngOnInit(): void {
    timer(200).pipe(
      delay(1000),
      switchMap(() => this.basket$),
      switchMap((data) => this.basketService.getBasketProducts(data.id)),
      switchMap((data) =>
      this.createBasketViewModel(data, this.basketService.getCurrentBasketValue()))
    ).subscribe(data => {
       this.basketProducts = data;
    })
  };

  removeBasketItem(product: productViewModel){
    this.basketService.removeItemFromBasket(product);
  }

  incrementItemQuantity(product: productViewModel){
    this.basketService.incrementItemQuantity(product);
  }

  decrementItemQuantity(product: productViewModel){
    this.basketService.decrementItemQuantity(product);
  }

  private createBasketViewModel(products: IProduct[], basket: IBasket) {
    let product: IProduct;
    let basketItem: IBasketItem;
    let basketProducts: productViewModel[] = [];
    for (let i = 0; i < products.length; i++) {
      product = products[i];
      basketItem = basket.basketItems[i];
      let basketProduct = new productViewModel(products[i].id,
        products[i].name, products[i].price, basket.basketItems[i].quantity, products[i].pictures[0],
        products[i].productBrand, products[i].productType)
      basketProducts.push(basketProduct);
    }
    return new Observable <productViewModel[]>(observer => {
      observer.next(basketProducts);
      observer.complete();
    })
  }
}
