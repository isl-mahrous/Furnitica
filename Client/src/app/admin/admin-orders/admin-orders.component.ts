import { LabelType, Options } from '@angular-slider/ngx-slider';
import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { IOrder, orderParams } from 'src/app/shared/models/order';
import { IOrdersPagination } from 'src/app/shared/models/pagination';
import { AdminOrdersService } from './admin-orders.service';

@Component({
  selector: 'app-admin-orders',
  templateUrl: './admin-orders.component.html',
  styleUrls: ['./admin-orders.component.scss']
})
export class AdminOrdersComponent implements OnInit {


  orders : IOrder[] = [];
  totalCount: number;
  searchOption = [{name: "Pending", value: "0"}, {name: "Payment Recieved", value: "1"}, {name: "Payment Failed", value: "2"}];
  sortOptions = [{name: "lower subtotal to higher subtotal", value: "subTotalAsc"}, {name: "higher subtotal to lower subtotal", value: "subTotalDesc"}];
  orderParamsToSend : orderParams;
  pageSize : number = 6;

  minValue = 0;
  maxValue = 1000000000;

  options: Options = {
    floor: 0,
    ceil: this.maxValue,
    enforceRange: true,
    translate: (value: number, label: LabelType): string => {
      switch (label) {
        case LabelType.Low:
          return "$" + value;
        case LabelType.High:
          return "$" + value;
        default:
          return "$" + value;
      }
    }
  };


  constructor(private adminOrdersService : AdminOrdersService) { }

  ngOnInit(): void {
    this.orderParamsToSend = new orderParams();
    this.orderParamsToSend.pageIndex = 1;
    this.orderParamsToSend.pageSize = 6;
    this.orderParamsToSend.sort = "subTotalAsc";
    this.orderParamsToSend.subTotalFrom = 0;
    this.getOrders();
  }

  private getOrders() {

    this.adminOrdersService.getAllOrders(this.orderParamsToSend).subscribe(
      {
        next : (response : IOrdersPagination) => {
          console.log(response);
          this.orders = response.data;
        },
        error : err => console.log(err)
      }
    )

  }

  priceRangeChanged() {
    this.orderParamsToSend.subTotalFrom = this.minValue;
    this.orderParamsToSend.subTotalTo = this.maxValue;
    this.orderParamsToSend.pageIndex = 1;
    this.getOrders();
  }

  getMaxPrice() {
    this.adminOrdersService.getMaxPrice().subscribe({
      next: res => {
        this.maxValue = res;
        const newOptions: Options = Object.assign({}, this.options);
        newOptions.ceil = this.maxValue;
        this.options = newOptions;
      },
      error: err => { console.log(err); }
    })
  }

  openDialog() {

  }

  showDetails(order : IOrder) {

  }

  editOrder(order : IOrder) {

  }

  confrimDelete(orderId : number) {

  }

  onSortSelected(e : Event) {
    this.orderParamsToSend.sort = (e.target as HTMLSelectElement).value;
    this.getOrders();
  }

  onFilterSelected(e : Event) {
    let searchIndex = +(e.target as HTMLSelectElement).value;
    if(searchIndex == 3)
    {
      this.clearFilters()
    }
    else {
      this.orderParamsToSend.search = searchIndex;
    }
    this.getOrders();
  }

  onPageChanged(event: PageEvent) {
    this.orderParamsToSend.pageIndex = event.pageIndex+1;
    this.getOrders();
  }

  private clearFilters() {
    this.orderParamsToSend = new orderParams();
    this.orderParamsToSend.pageIndex = 1;
    this.orderParamsToSend.pageSize = 6;
    this.orderParamsToSend.subTotalFrom = 0;
    this.getOrders();
    this.getMaxPrice();
  }

}
