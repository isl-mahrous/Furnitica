import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { AccountService } from './../../account/account.service';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/shared/models/user';
import { NavBarSearchService } from './nav-bar-search.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  basket$: Observable<IBasket>;
  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;

  currentUser$: Observable<IUser>;
  @ViewChild("search", { static: false }) searchTerm: ElementRef;

  constructor(private accountService: AccountService, private navBarService: NavBarSearchService) { }

  ngOnInit(): void {

    this.currentUser$ = this.accountService.currentUser$
  }

  onSearch() {
    this.navBarService.searchSource.next(this.searchTerm.nativeElement.value);
  }
}
