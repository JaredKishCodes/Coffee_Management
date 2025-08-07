import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AddCartDto, CartResponse, AddCartItemRequest } from '../../models/cart.model';
import { AddCartItemDto } from '../../models/carItem.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  
  http = inject(HttpClient)
  
  apiUrl = 'https://localhost:7168/api/Cart';
  
  addToCart(cart: AddCartDto): Observable<CartResponse> {
    return this.http.post<CartResponse>(`${this.apiUrl}/AddCart`, cart);
  }

  createCart(customerName: string, cartItem: AddCartItemDto): Observable<CartResponse> {
    const addCartItemRequest: AddCartItemRequest = {
      coffeeItemId: cartItem.coffeeItemId,
      quantity: cartItem.quantity
    };
    
    const addCartDto: AddCartDto = {
      customerName: customerName,
      cartItems: [addCartItemRequest]
    };
    return this.http.post<CartResponse>(`${this.apiUrl}/AddCart`, addCartDto);
  }
}
