import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: "shop", loadChildren: () => import("./shop/shop.module").then(mod => mod.ShopModule), data: { breadcrumb: "Shop" } },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
