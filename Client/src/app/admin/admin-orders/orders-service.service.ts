import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrdersServiceService {


  baseUrl = environment.apiUrl;

  constructor(private http : HttpClient) { }

  getAllOrders() {
    this.http.get(this.baseUrl + "AdminOrders");
  }
}
