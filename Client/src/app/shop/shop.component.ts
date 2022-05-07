import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  totalCount: number;
  shopParams: ShopParams = new ShopParams();
  sortOptions = [
    { name: "Name, A to Z", value: "nameAsc" },
    { name: "Name, Z to A", value: "nameDesc" },
    { name: "Price: Low to High", value: "priceAsc" },
    { name: "Price: High to Low", value: "priceDesc" }
  ];
  @ViewChild("search", { static: false }) searchTerm: ElementRef;

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }


  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe(res => {
      this.products = res.data;
      this.shopParams.pageIndex = res.pageIndex;
      this.shopParams.pageSize = res.pageSize;
      this.totalCount = res.count;
    }, err => {
      console.log(err);
    })
  }

  getBrands() {
    this.shopService.getBrands().subscribe({
      next: res => { this.brands = [{ id: 0, name: "All", origin: "Any" }, ...res] },
      error: err => { console.log(err); }
    })
  }

  getTypes() {
    this.shopService.getTypes().subscribe({
      next: res => { this.types = [{ id: 0, name: "All" }, ...res]; },
      error: err => { console.log(err); }
    })
  }

  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }

  onSortOptionChanged(event: any) {
    this.shopParams.sort = event.target.value;
    this.getProducts();
  }

  sortNameAsc() {
    this.shopParams.sort = "nameAsc";
    this.getProducts();
  }

  sortNameDesc() {
    this.shopParams.sort = "nameDesc";
    this.getProducts();
  }

  sortPriceAsc() {
    this.shopParams.sort = "priceAsc";
    this.getProducts();
  }

  sortPriceDesc() {
    this.shopParams.sort = "priceDesc";
    this.getProducts();
  }

  onPageChanged(event: any) {
    if (this.shopParams.pageIndex != event.page) {
      this.shopParams.pageIndex = event.page;
      this.getProducts();
    }
  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }

  onSearchReset() {
    this.searchTerm.nativeElement.value = "";
    this.shopParams = new ShopParams();
    this.getProducts();
  }

}
