import { environment } from './../../environments/environment';
import { IUser } from './../shared/models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<IUser>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {
    // this.currentUser$ = null
  }

  getCurrentUserValue() {

    return this.currentUserSource.value;
  }

  loadCurrentUser(token: string) {

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`)

    return this.http.get(this.baseUrl + 'account/getuser', { headers }).pipe(
      map((user: IUser) => {

        if (user) {

          // localStorage.setItem('token', user.token);
          this.currentUserSource.next(user)
        }
      })
    )
  }


  login(values: any) {

    return this.http.post(this.baseUrl + 'Account/login', values).pipe(
      map((user: IUser) => {

        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          this.router.navigateByUrl('/');
        }
      })
    );
  }


  register(values: any) {

    return this.http.post(this.baseUrl + 'Account/register', values).pipe(
      map((response) => {
        console.log(response)
        this.assignRoleToUser(
          {
            RoleName: 'Customer',
            UserId: response['userId'],
            Action: 1
          }
        ).subscribe(() => {

          console.log('role assigned');
        })
      })
    );
  }

  assignRoleToUser(values: any) {

    return this.http.post(this.baseUrl + 'Role/AssignRole', values).pipe(
      map((response) => {
        console.log(response)
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }


  changeProfileImage(values) {

    return this.http.post(this.baseUrl + 'Account', values).pipe(
      map((response) => {

        console.log(response)
      })
    )
  }
}
