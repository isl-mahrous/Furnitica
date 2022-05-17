import { AccountService } from 'src/app/account/account.service';
import { Component, OnInit } from '@angular/core';
import { IUser } from 'src/app/shared/models/user';
import { Observable } from 'rxjs';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  currentCustomerUsers:IUser[];
  pageIndex:number;
  pageSize:number;
  count:number;
  userId:string;
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.pageIndex=1;
   this.pageSize=6;
   this.getUsers();

  }
  onPageChanged(event: PageEvent){
    this.pageIndex = event.pageIndex+1;
    this.getUsers();

  }


  getUsers(){
    this.accountService.loadCustomerUsers({pageIndex:this.pageIndex,pageSize:this.pageSize}).subscribe({
      next: (response) => {
        this.currentCustomerUsers = response.data;
        this.pageIndex=response.pageIndex;
        this.pageSize=response.pageSize;
        this.count=response.count;
        console.log(response)
      },
      error:(err)=>{
        console.log(err);

      }
    })
  }

  confirm(userId:string){
    if(confirm("Are you sure want to make this user admin")) {
      this.assginRole(userId);
  }
}

  assginRole(userId:string){


    this.accountService.assignRoleToUser({UserId:userId,RoleName:"Customer",Action:0}).subscribe({
      next:(res)=>{
        this.accountService.assignRoleToUser({UserId:userId,RoleName:"Admin",Action:1}).subscribe({
          next:(res)=>{
            this.pageIndex=1;
            this.pageSize=6;
            this.getUsers();
          },
          error:(err)=>{
            console.log(err);
          }

        });

      },
      error:(err)=>{
        console.log(err);
      }



    });

  }

}
