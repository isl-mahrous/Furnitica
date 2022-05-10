import { AccountService } from './account/account.service';
import { Component, OnInit } from '@angular/core';
import { IUser } from './shared/models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  currentUser$: IUser;
  title = 'Furnitica';

  constructor(private accountService: AccountService, public router: Router) {

  }
  ngOnInit(): void {

    this.loadCurrentUser()
  }

  loadCurrentUser() {

    const token = localStorage.getItem('token');
    if (token) {
      let result = this.accountService.loadCurrentUser(token).subscribe(() => {
        console.log('loaded user')
        console.log(this.accountService.getCurrentUserValue())
      }, error => {
        console.log(error)
      })
      console.log(result)
    }
  }
}
