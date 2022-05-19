import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { IOrder, orderParams } from 'src/app/shared/models/order';
import { IOrdersPagination } from 'src/app/shared/models/pagination';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminOrdersService {

  baseUrl = environment.apiUrl;

  constructor(private http : HttpClient) { }

  getAllOrders(orderParamsToSend : orderParams) {

    let params = new HttpParams();

    if (orderParamsToSend.subTotalFrom !== 0) {
      params = params.append("SubTotalFrom", orderParamsToSend.subTotalFrom.toString());
    }

    if (orderParamsToSend.subTotalTo) {
      params = params.append("SubTotalTo", orderParamsToSend.subTotalTo.toString());
    }

    if (orderParamsToSend.sort) {
      params = params.append("Sort", orderParamsToSend.sort);
    }

    if (orderParamsToSend.search) {
      params = params.append("Search", orderParamsToSend.search);
    }

    params = params.append("PageIndex", orderParamsToSend.pageIndex);
    params = params.append("PageSize", orderParamsToSend.pageSize);

    return this.http.get<IOrdersPagination>(this.baseUrl + "AdminOrders", {
      observe: "response",
      params: params
    }).pipe(
      map(response => {
        console.log(response);
        return response.body;
      })
    )
  }

  getMaxPrice() {
    return this.http.get<number>(this.baseUrl + "AdminOrders/maxPrice");
  }

  deleteOrder(id){
    let params = new HttpParams();
    params = params.append("id", id.toString());
    return this.http.delete<IOrder>(this.baseUrl + "Orders", {
      observe: "response",
      params: params,
    });
  }

  getOrder(id : number) {
    return this.http.get<IOrder>(this.baseUrl + "AdminOrders/" + id);
  }
}
