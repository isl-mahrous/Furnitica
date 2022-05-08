import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from 'src/app/account/account.service';

@Injectable({
    providedIn: 'root'
})
export class AuthAccessGuard implements CanActivate {
    constructor(private accountService: AccountService, private router: Router) { }

    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean> {

        return this.accountService.currentUser$.pipe(
            map(auth => {
                console.log(auth)
                console.log(this.accountService.getCurrentUserValue())
                if (auth) {
                    console.log(auth)
                    return false;
                }
                else {
                    return true
                }
            })
        )
    }
}