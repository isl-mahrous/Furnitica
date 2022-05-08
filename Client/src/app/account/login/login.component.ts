import { AccountService } from './../account.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    if (this.accountService.currentUser$) {

      this.router.navigateByUrl('/');
    }
    this.createLoginForm();
  }

  createLoginForm() {

    this.loginForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required)
    });
  }

  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe(() => {

      console.log('user logged in')
    },
      error => {
        console.log(error)
      })
    console.log(this.loginForm.value)
  }

}
