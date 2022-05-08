import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagerComponent } from './components/pager/pager.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    PagerComponent,
    PagingHeaderComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    ReactiveFormsModule
  ],
  exports: [
    PagerComponent,
    PaginationModule,
    PagingHeaderComponent,
    ReactiveFormsModule
  ]
})
export class SharedModule { }
