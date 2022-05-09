import { AuthAccessGuard } from './core/guards/AuthAccess.guard';
import { ShopComponent } from './shop/shop.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [

  {path: 'basket', loadChildren: () => import('./basket/basket.module')
  .then(mod => mod.BasketModule), data: { breadcrumb: "Basket"  } 
  },
  {path: 'checkout', loadChildren: () => import('./checkout/checkout.module')
  .then(mod => mod.CheckoutModule), data: { breadcrumb: "Checkout"  } 
  },
  {
    path: "",
    loadChildren: () => import("./home/home.module")
      .then(mod => mod.HomeModule), data: { breadcrumb: "Home" }
  },

//   {
//     path: 'basket', loadChildren: () => import('./basket/basket.module')
//       .then(mod => mod.BasketModule), data: { breadcrumb: { skip: true } }
//   },
  {
    path: "shop",
    loadChildren: () => import("./shop/shop.module")
      .then(mod => mod.ShopModule), data: { breadcrumb: "Shop" }
  },
  {
    path: "account",
    loadChildren: () => import("./account/account.module")
      .then(mod => mod.AccountModule), data: { breadcrumb: { skip: true } }
  },
  
  { path: "**", redirectTo: "", pathMatch: "full" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
