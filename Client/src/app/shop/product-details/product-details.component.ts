import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product:IProduct;
  quantity:number=1;

  ratingList: boolean[] = [true,true,true,true,true];
  rating:number=0;

  constructor(private shopService:ShopService,private activeRoute:ActivatedRoute) { }

  ngOnInit(): void {
    this.loadProduct();

  }

  loadProduct(){
      this.shopService.getProduct(+this.activeRoute.snapshot.paramMap.get("id")).subscribe(product=>{

        this.product=product;
      },error=>{
        console.log(error);
      }

      );
  }

  increaseQuantity(){
    if(this.quantity+1>this.product.unitsInStock){
        this.quantity=this.product.unitsInStock;
    }
    else{
      this.quantity++;
    }
  }
  decreaseQuantity(){

    if(this.quantity-1==0){
      this.quantity=1;
  }
  else{
    this.quantity--;
  }

  }


  setStar(data:any){
    this.rating=data+1;
    for(var i=0;i<=4;i++){
      if(i<=data){
        this.ratingList[i]=false;
      }
      else{
        this.ratingList[i]=true;
      }
   }
   console.log(this.rating);
}




}
