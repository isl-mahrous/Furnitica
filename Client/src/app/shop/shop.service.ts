import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl: string = "https://localhost:7272/api"

  constructor(private http: HttpClient) {

  }

  getProducts() {

    return this.http.get(this.baseUrl + '/products?pageSize=5');
  }
}
