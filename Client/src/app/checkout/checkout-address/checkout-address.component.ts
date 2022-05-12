import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { AccountService } from 'src/app/account/account.service';


@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent implements OnInit {

  @Input() checkoutForm : FormGroup;

  constructor() { }

  ngOnInit(): void {
  }

  // saveUserAddress(){
  //   this.accountService.updateUserAddress(this.checkoutForm.get('addressForm').value).subscribe({
  //     next : (address) => {
  //       this.toastr.success("Address Saved");
  //     },
  //     error : (err) => {
  //       this.toastr.error("Error Occured, Address is not Saved");
  //       console.log(err);
  //     }
  //   });
  // }

}
