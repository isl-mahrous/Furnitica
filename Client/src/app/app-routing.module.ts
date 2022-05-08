import { ShopComponent } from './shop/shop.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: 'basket', loadChildren: () => import('./basket/basket.module').then(mod =>
    mod.BasketModule)}
  {
    path: "shop",
    loadChildren: () => import("./shop/shop.module")
      .then(mod => mod.ShopModule), data: { breadcrumb: "Shop" }
  },
  {
    path: "account",
    loadChildren: () => import("./account/account.module")
      .then(mod => mod.AccountModule), data: { breadcrumb: { skip: true } }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
