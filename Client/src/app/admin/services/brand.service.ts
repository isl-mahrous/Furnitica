import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

import { HttpClient,HttpHeaders ,HttpParams } from '@angular/common/http';
import { IBrandPagination } from 'src/app/shared/models/brandPagination';
import { IBrand } from 'src/app/shared/models/brand';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BrandService {
  baseUrl: string = environment.apiUrl;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  constructor(private http: HttpClient) { }


  getBrandsPagination(paginationData:any) {
    let params = new HttpParams();

    params = params.append("pageIndex", paginationData.pageIndex);
    params = params.append("pageSize", paginationData.pageSize);

    return this.http.get<IBrandPagination>(this.baseUrl + "ProductBrands/", {
      observe: "response",
      params: params

    }).pipe(
      map(response => {
        return response.body;
      })
    )

  }


}
