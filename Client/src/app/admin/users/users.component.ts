import { AccountService } from 'src/app/account/account.service';
import { Component, OnInit } from '@angular/core';
import { IUser } from 'src/app/shared/models/user';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  currentCustomerUsers$: Observable<IUser[]>;
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {

    this.accountService.loadCustomerUsers().subscribe({
      next: (response) => {
        this.currentCustomerUsers$ = this.accountService.currentCustomerUsers$
      }
    })
  }

}
