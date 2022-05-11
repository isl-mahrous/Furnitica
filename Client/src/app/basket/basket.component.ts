import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
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

  constructor(private basketService: BasketService) { }

  ngOnInit() {
    this.basket$ = this.basketService.basket$;
  }

  removeBasketItem(basketItem: IBasketItem) {
    var item  = new productViewModel(basketItem.productId, "product Name", basketItem.price, basketItem.quantity , "Picture Url", "Brand Name", "Type");
    this.basketService.removeItemFromBasket(item);
  }

  incrementItemQuantity(basketItem: IBasketItem) {
    var item  = new productViewModel(basketItem.productId, "product Name", basketItem.price, basketItem.quantity , "Picture Url", "Brand Name", "Type");
    this.basketService.incrementItemQuantity(item);
  }

  decrementItemQuantity(basketItem: IBasketItem) {
    var item  = new productViewModel(basketItem.productId, "product Name", basketItem.price, basketItem.quantity , "Picture Url", "Brand Name", "Type");
    this.basketService.decrementItemQuantity(item);
  }
}
