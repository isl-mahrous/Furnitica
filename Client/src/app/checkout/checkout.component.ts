import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { delay, Observable, switchMap, timer } from 'rxjs';
import { AccountService } from '../account/account.service';
import { BasketService } from '../basket/basket.service';
import { IBasket } from '../shared/models/basket';
import { IDeliveryMethod } from '../shared/models/deliveryMethod';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

  checkoutForm : FormGroup;

  constructor(private fb : FormBuilder, private basketService:BasketService) { }

  ngOnInit(): void {
    this.createCheckoutForm();
    timer(2500).pipe(
      delay(1500),
      switchMap(() => this.basketService.basket$),
      switchMap((basket) => this.getDeliveryMethodValue(basket)),
    ).subscribe();  
  }

  createCheckoutForm()
  {
    this.checkoutForm = this.fb.group({

    addressForm : this.fb.group(
      {
        firstName : [null, Validators.required],
        lastName : [null, Validators.required],
        street : [null, Validators.required],
        city : [null, Validators.required],
        state : [null, Validators.required],
        zipcode : [null, Validators.required],
      }),

      deliveryForm : this.fb.group({
        deliveryMethod : [null, Validators.required]
      }),

      paymentForm : this.fb.group({
        nameOnCard : [null, Validators.required]
      }),

    });
  }
  getDeliveryMethodValue(basket : IBasket) : Observable<void> {
    if(basket.deliveryMethodId !== null){
      this.checkoutForm.get('deliveryForm').get('deliveryMethod')
      .patchValue(basket.deliveryMethodId.toString());
    }
    return new Observable<void>(observer => {
      observer.next()
      observer.complete();
    })
  }
}
