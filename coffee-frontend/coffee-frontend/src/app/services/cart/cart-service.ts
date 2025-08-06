import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AddCartDto, CartResponse } from '../../models/cart.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  
  http = inject(HttpClient)
  
  apiUrl = 'https://localhost:7168/api/Cart';
  
  addToCart(cart: AddCartDto):Observable<CartResponse> {
    return this.http.post<CartResponse>(`${this.apiUrl}/AddCart`, cart);
  }
}
