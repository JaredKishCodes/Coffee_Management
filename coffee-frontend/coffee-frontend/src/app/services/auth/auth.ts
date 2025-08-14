import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, from, Observable, tap } from 'rxjs';
import { IAuthResponse, ILogin, IRegister} from '../../models/auth.model';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUrl = 'https://localhost:7168/api/Account/';

  private loggedIn = new BehaviorSubject<boolean>(localStorage.getItem('auth')? true : false);

  isLoggedIn$ = this.loggedIn.asObservable();

  constructor(private http: HttpClient) {}

  login(creds:ILogin) :Observable<IAuthResponse> {
     
    return this.http.post<IAuthResponse>(`${this.apiUrl}login`, creds).pipe(
      tap((res)=>{
        localStorage.setItem('auth',JSON.stringify(res))
        this.loggedIn.next(true)
      })
    );
  }

  register(creds:IRegister) :Observable<IAuthResponse> {
    return this.http.post<IAuthResponse>(`${this.apiUrl}register`, creds);
  }

}
