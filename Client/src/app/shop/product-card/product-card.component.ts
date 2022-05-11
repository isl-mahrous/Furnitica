import { AccountService } from 'src/app/account/account.service';
import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/product';
import { IWishList } from 'src/app/shared/models/wishlist';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {
  @Input() product: IProduct;
  wishListItems: IWishList;
  currentWishList$: any;
  inWishList: boolean = false;
  constructor(private basketService: BasketService, private accountService: AccountService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    if (localStorage.getItem('token') !== null) {
      this.wishListItems = this.accountService.getCurrentWishListValue()
      this.checkInWishList()
    }

  }

  addItemToBasket() {
    this.basketService.addItemToBasket(this.product);
  }

  addToWishList() {
    let token = localStorage.getItem('token')
    if (token) {

      this.accountService.addToWishList(token, this.product.id)
      this.inWishList = true
    }
    else {
      console.log('you are not logged in')
    }
  }

  removeFromWishList() {

    let token = localStorage.getItem('token')
    if (token) {
      console.log(token)
      this.accountService.removeFromWishList(token, this.product.id)
      this.inWishList = false
    }
    else {
      console.log('you are not logged in')
    }
  }

  checkInWishList() {
    if (localStorage.getItem('token') !== null) {

      this.inWishList = this.accountService.checkInWishList(this.product.id)
      console.log(this.inWishList)
    }
  }
}
