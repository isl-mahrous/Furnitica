import { Component, OnInit } from '@angular/core';
import { delay, Observable, switchMap, timer } from 'rxjs';
import { IBasket, IBasketItem } from '../shared/models/basket';
import { productViewModel } from '../shared/viewmodels/product-viewmodel';
import { BasketService } from './basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {
  basket$: Observable<IBasket>;
  basketProducts: any;

  constructor(private basketService: BasketService) { }

  // ngOnInit() {
  //   this.basket$ = this.basketService.basket$;
  //  };

  ngOnInit(): void {
    timer(2000).pipe(
      delay(1000),
      switchMap(() => this.basket$),
      switchMap((data) => this.basketService.getBasketProducts(data.id)),
      switchMap((data) =>
      this.createBasketViewModel(data, this.basketService.getCurrentBasketValue()))
    ).subscribe(data => {
       this.basketProducts = data;
    })
  }

  createBasketViewModel(data: import("../shared/models/product").IProduct[], arg1: IBasket): any {
    throw new Error('Method not implemented.');
  }
;

  removeBasketItem(product: productViewModel){
    this.basketService.removeItemFromBasket(product);
  }

  // removeBasketItem(basketItem: IBasketItem) {
  //   var item  = new productViewModel(basketItem.productId, "product Name", basketItem.price, basketItem.quantity , "Picture Url", "Brand Name", "Type");
  //   this.basketService.removeItemFromBasket(item);
  // }

  incrementItemQuantity(basketItem: IBasketItem) {
    var item  = new productViewModel(basketItem.productId, "product Name", basketItem.price, basketItem.quantity , "Picture Url", "Brand Name", "Type");
    this.basketService.incrementItemQuantity(item);
  }

  decrementItemQuantity(basketItem: IBasketItem) {
    var item  = new productViewModel(basketItem.productId, "product Name", basketItem.price, basketItem.quantity , "Picture Url", "Brand Name", "Type");
    this.basketService.decrementItemQuantity(item);
  }
}
