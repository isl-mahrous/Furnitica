import { AccountService } from './account/account.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Furnitica';

  constructor(private accountService: AccountService) {

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
