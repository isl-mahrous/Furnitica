import { SharedModule } from './../shared/shared.module';
import { AccountRoutingModule } from './account-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { WishlistComponent } from './wishlist/wishlist.component';




@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    WishlistComponent

  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    SharedModule,
    ModalModule.forRoot()
  ]
})
export class AccountModule { }
