import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CartDto } from '../models/cart.model';

@Injectable({
  providedIn: 'root'
})

export class CartService {
  http = inject(HttpClient)

  apiUrl = 'https://localhost:7168/api/Cart';

   getCartById(id:number):Observable<CartDto>{
    return this.http.get<CartDto>(`${this.apiUrl}/GetCartById/${id}`)
   }
}
