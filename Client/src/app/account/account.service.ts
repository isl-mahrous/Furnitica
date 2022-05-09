import { IUser } from './../shared/models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
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
      map((user: IUser) => {

        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
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
