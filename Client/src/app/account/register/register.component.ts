import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    // if (this.accountService.getCurrentUserValue() !== null) {

    //   this.router.navigateByUrl('/');
    // }
    // console.log(this.accountService.getCurrentUserValue())
    this.createRegisterForm();
  }

  createRegisterForm() {

    this.registerForm = new FormGroup({
      username: new FormControl('', [Validators.required]),
      password: new FormControl('', Validators.required)
    });
  }

  onSubmit() {
    this.accountService.login(this.registerForm.value).subscribe(() => {

      console.log('user logged in')
    },
      error => {
        console.log(error)
      })
    console.log(this.registerForm.value)
  }

}
