import { Component, OnInit } from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import { DialogComponent } from '../dialog/dialog.component';


import { ProductService } from '../services/product.service';

import { IProduct } from 'src/app/shared/models/product';
import { ShopParams } from 'src/app/shared/models/shopParams';

import { PageEvent} from '@angular/material/paginator';

import {MatTableDataSource} from '@angular/material/table';
import { IBrand } from 'src/app/shared/models/brand';
import { IType } from 'src/app/shared/models/productType';
import { BrandService } from '../services/brand.service';

@Component({
  selector: 'app-brands',
  templateUrl: './brands.component.html',
  styleUrls: ['./brands.component.scss']
})
export class BrandsComponent implements OnInit {
  pageIndex:number;
  pageSize:number;
  count:number;
  brands: IBrand[];

  constructor(private dialog:MatDialog,private brandService:BrandService) { }

  ngOnInit(): void {
    this.pageIndex=1;
    this.pageSize=6;
    this.getBrands();
  }

  getBrands(){
    this.brandService.getBrandsPagination({pageIndex:this.pageIndex,pageSize:this.pageSize}).subscribe({
      next:(res)=>{

        this.brands = res.data;
        this.pageIndex=res.pageIndex;
        this.pageSize=res.pageSize;
        this.count=res.count;
        console.log(res);
      },
      error:(err)=>{
        console.log(err);
      }


    })

  }

  onPageChanged(event: PageEvent){
    this.pageIndex = event.pageIndex+1;
    this.getBrands();

  }
}
