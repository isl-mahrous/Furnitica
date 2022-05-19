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


  orders : IOrder[];
  length : number;
  totalCount: number;
  searchOption = ["Pending", "Payment Recieved", "Payment Failed"];
  sortOptions = [{name: "Lower Cost To Higher", value: "subTotalAsc"}, {name: "Higher Cost To Lower", value: "subTotalDesc"}];
  orderParamsToSend : orderParams;
  pageSize : number = 6;




  minValue = 0;
  maxValue = 100000;

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
  orderDetails: IOrder;


  constructor(private adminOrdersService : AdminOrdersService) { }

  ngOnInit(): void {
    this.orderParamsToSend = new orderParams();
    this.orderParamsToSend.pageIndex = 1;
    this.orderParamsToSend.pageSize = 6;
    this.orderParamsToSend.sort = "subTotalAsc";
    this.orderParamsToSend.search;
    this.getMaxPrice();
    this.orderParamsToSend.subTotalFrom = 0;
    this.getOrders();
  }

  private getOrders() {

    this.adminOrdersService.getAllOrders(this.orderParamsToSend).subscribe(
      {
        next : (response : IOrdersPagination) => {
          this.orders = response.data;
          this.orderParamsToSend.pageIndex = response.pageIndex;
          this.orderParamsToSend.pageSize = response.pageSize;
          this.totalCount = response.count;
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
        console.log(this.maxValue);
      },
      error: err => { console.log(err); }
    })
  }

  openDialog() {

  }

  showDetails(orderId : number) {

  }

  editOrder(order : IOrder) {

  }

  confrimDelete(id:number) {
    if(confirm("Are you sure to delete ")) {
      this.deleteOrder(id);
    }
  }

  private deleteOrder(id:number){
    this.adminOrdersService.deleteOrder(id).subscribe({
      next: response => console.log(response)
    });
    this.getOrders();
  }

  onSortSelected(e : Event) {
    this.orderParamsToSend.sort = (e.target as HTMLSelectElement).value;
    this.orderParamsToSend.pageIndex = 1;
    this.getOrders();
  }

  onFilterSelected(e : Event) {
    let filterValue = (e.target as HTMLSelectElement).value;
    if(filterValue === "all")
    {
      this.clearFilters();
    }
    else {
      this.orderParamsToSend.search = filterValue;
      this.orderParamsToSend.pageIndex = 1;
    }
    console.log(filterValue);
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
    this.getMaxPrice();
    this.orderParamsToSend.subTotalFrom = 0;
    this.getOrders();
  }

}
