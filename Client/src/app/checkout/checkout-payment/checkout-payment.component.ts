import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { IOrder } from 'src/app/shared/models/order';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements OnInit {

  @Input() checkoutForm : FormGroup;

  constructor(private basketService : BasketService,
    private checkoutService : CheckoutService,
    private toastrService : ToastrService, private router : Router) {
  }

  submitOrder() {
    const basket = this.basketService.getCurrentBasketValue();
    const orderToCreate = this.getOrderToCreate(basket);
    this.checkoutService.creatOrder(orderToCreate).subscribe({
      next : (order : IOrder) => {
        this.toastrService.success('Order Created Successfully');
        this.basketService.deleteLocalBasket();
        const navigationExtras : NavigationExtras = {state: order};
        this.router.navigate(['checkout/success'], navigationExtras);
      },
      error : (err) => {
        this.toastrService.error(err.message);
        console.log(err);
      }
    })


  }
  getOrderToCreate(basket: IBasket) {
    return {
      basketId : basket.id,
      deliveryMethodId : +this.checkoutForm.get('deliveryForm').get('deliveryMethod').value,
      shipToAddress : this.checkoutForm.get('addressForm').value
    };
  }

  ngOnInit(): void {
  }

}
