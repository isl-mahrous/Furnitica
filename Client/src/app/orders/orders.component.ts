import { Component, OnInit } from '@angular/core';
import { OrdersService } from './orders.service';
import { IOrder } from '../shared/models/order';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
  orders: IOrder[];

  constructor(private ordersService: OrdersService, private toastr : ToastrService) { }

  ngOnInit() {
    this.getOrders();
  }

  getOrders() {
    this.ordersService.getOrdersForUser().subscribe((orders: IOrder[]) => {
      this.orders = orders;
    }, error => {
      console.log(error);
    });
  }

  showDetails(orderID : number){

  }

  deleteOrder(orderID : number){
   this.ordersService.deleteOrder(orderID).subscribe({
     next : data => {
       this.toastr.success("Order deleted");
       this.getOrders()
      },
       error : err => this.toastr.error("Error! Order wasn't deleted"),
   })
  }

}
