import { IWishList } from './../../shared/models/wishlist';
import { AccountService } from 'src/app/account/account.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/shared/models/user';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.scss']
})
export class WishlistComponent implements OnInit {
  currentUser$: Observable<IUser>;
  currentWishList$: IWishList;


  constructor(private accountService: AccountService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    console.log(this.accountService.getCurrentUserValue().userId)
    this.route.data.subscribe(() => {
      this.currentWishList$ = this.accountService.getCurrentWishListValue();
    });
  }


  removeFromWishList(productId: number) {

    let token = localStorage.getItem('token')
    if (token) {
      console.log(token)
      this.accountService.removeFromWishList(token, productId)
      let newList = this.currentWishList$.products.filter(element => {
        return element.id !== productId
      })
      this.currentWishList$.products = newList
    }
    else {
      console.log('you are not logged in')
    }
  }
}
