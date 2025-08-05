import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddCartItemDto, CartItemDto } from '../../models/carItem.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartItemService {
  
 apiUrl = 'https://localhost:7168/api/CartItem';

 constructor(private http : HttpClient){
 }

 addCartItem(item: AddCartItemDto) : Observable<CartItemDto> {
   return this.http.post<CartItemDto>(`${this.apiUrl}/AddCartItem`, item);
 }

 getCartItemById(id: number): Observable<CartItemDto> {
  return this.http.get<CartItemDto>(`${this.apiUrl}/GetCartItemById?id=${id}`);
 }
}
