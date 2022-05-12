import { AfterViewInit, Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { IOrder } from 'src/app/shared/models/order';
import { CheckoutService } from '../checkout.service';

declare var Stripe;

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements AfterViewInit, OnDestroy {

  @Input() checkoutForm: FormGroup;

  @ViewChild('cardNumber', { static: true }) cardNumberElement: ElementRef;
  @ViewChild('cardExpiry', { static: true }) cardExpiryElement: ElementRef;
  @ViewChild('cardCvc', { static: true }) cardCvcElement: ElementRef;

  stripe: any;
  cardNumber: any;
  cardExpiry: any;
  cardCvc: any;
  cardErrors: any;
  cardHandler = this.onChange.bind(this);

  constructor(private basketService: BasketService,
    private checkoutService: CheckoutService,
    private toastrService: ToastrService, private router: Router) {
  }
  ngOnDestroy(): void {
    this.cardNumber.destroy();
    this.cardCvc.destroy();
    this.cardCvc.destroy();
  }
  ngAfterViewInit(): void {
    this.stripe = Stripe('pk_test_51KyZpGLfkfZnEzSoBeteIizvBg64NexZoo3JpUoNqTuU9HIjw0GQCUo5ewqKG6OrqeHH4PZzZh4QL95lh1rU5cF500InZaAte0');
    const elements = this.stripe.elements();

    this.cardNumber = elements.create('cardNumber');
    this.cardNumber.mount(this.cardNumberElement.nativeElement);
    this.cardNumber.addEventListener('change', this.cardHandler)

    this.cardExpiry = elements.create('cardExpiry');
    this.cardExpiry.mount(this.cardExpiryElement.nativeElement);
    this.cardExpiry.addEventListener('change', this.cardHandler)

    this.cardCvc = elements.create('cardCvc');
    this.cardCvc.mount(this.cardCvcElement.nativeElement);
    this.cardCvc.addEventListener('change', this.cardHandler)
  }

  onChange({ error }) {
    if (error) {
      this.cardErrors = error.message;
    }
    else {
      this.cardErrors = null;
    }
  }
  submitOrder() {
    const basket = this.basketService.getCurrentBasketValue();
    const orderToCreate = this.getOrderToCreate(basket);
    this.checkoutService.creatOrder(orderToCreate).subscribe({
      next: (order: IOrder) => {
        this.toastrService.success('Order Created Successfully');
        this.stripe.confirmCardPayment(basket.clientSecret, {
          payment_method: {
            card: this.cardNumber,
            billing_details: {
              name: this.checkoutForm.get('paymentForm').get('nameOnCard').value
            }
          }
        }).then(result => {
          console.log(result);
          if (result.paymentIntent) {
            this.basketService.deleteLocalBasket();
            const navigationExtras: NavigationExtras = { state: order };
            this.router.navigate(['checkout/success'], navigationExtras);
          }
          else {
            this.toastrService.error('Payment error');
          }
        });
      },
      error: (err) => {
        this.toastrService.error(err.message);
        console.log(err);
      }
    })


  }
  getOrderToCreate(basket: IBasket) {
    return {
      basketId: basket.id,
      deliveryMethodId: +this.checkoutForm.get('deliveryForm').get('deliveryMethod').value,
      shipToAddress: this.checkoutForm.get('addressForm').value
    };
  }

}
