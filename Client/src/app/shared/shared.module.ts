import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagerComponent } from './components/pager/pager.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown'

@NgModule({
  declarations: [
    PagerComponent,
    PagingHeaderComponent,
    OrderTotalsComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    BsDropdownModule.forRoot(),
    ReactiveFormsModule
  ],
  exports: [
    PagerComponent,
    PaginationModule,
    PagingHeaderComponent,
    ReactiveFormsModule,
    BsDropdownModule
    OrderTotalsComponent,
    ReactiveFormsModule
  ]
})
export class SharedModule { }
