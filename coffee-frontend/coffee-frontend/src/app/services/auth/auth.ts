import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import { IAuthResponse, ILogin, IRegister} from '../../models/auth.model';
@Injectable({
  providedIn: 'root'
})
export class Auth {
  apiUrl = 'https://localhost:7168/api/Account/';

  constructor(private http: HttpClient) {}

  login(creds:ILogin) :Observable<IAuthResponse> {
    return this.http.post<IAuthResponse>(`${this.apiUrl}login`, creds);
  }

  register(creds:IRegister) :Observable<IAuthResponse> {
    return this.http.post<IAuthResponse>(`${this.apiUrl}register`, creds);
  }

}
