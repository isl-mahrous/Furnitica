import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagerComponent } from './components/pager/pager.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown'

import {CdkStepperModule} from '@angular/cdk/stepper';
import { StepperComponent } from './components/stepper/stepper.component';
import { TextInputComponent } from './components/text-input/text-input.component';

@NgModule({
  declarations: [
    PagerComponent,
    PagingHeaderComponent,
    OrderTotalsComponent,
    StepperComponent,
    TextInputComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    BsDropdownModule.forRoot(),
    ReactiveFormsModule,
    CdkStepperModule
  ],
  exports: [
    PagerComponent,
    PaginationModule,
    PagingHeaderComponent,
    ReactiveFormsModule,
    BsDropdownModule,
    OrderTotalsComponent,
    ReactiveFormsModule,
    CdkStepperModule,
    StepperComponent,
    TextInputComponent
  ]
})
export class SharedModule { }
