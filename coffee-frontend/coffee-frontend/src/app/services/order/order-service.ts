import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse, CreateOrderDto, OrderDto } from '../../models/order.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  
  http  = inject(HttpClient)
  apiUrl : string = 'https://localhost:7168/api/Order'

  getAllOrders():Observable<ApiResponse<OrderDto[]>>{
    return this.http.get<ApiResponse<OrderDto[]>>(`${this.apiUrl}/GetAllOrders`);
  }

  updateOrder(id:number,updateOrderDto:CreateOrderDto): Observable<ApiResponse<OrderDto>>{
   return this.http.put<ApiResponse<OrderDto>>(`${this.apiUrl}/UpdateOrder/${id}`,{updateOrderDto});
  }

  addOrder(order:CreateOrderDto):Observable<OrderDto>{
    return this.http.post<OrderDto>(`${this.apiUrl}/AddOrder`,order)
  }

  deleteOrder(id : number) : Observable<ApiResponse<OrderDto>>{
    return this.http.delete<ApiResponse<OrderDto>>(`${this.apiUrl}/DeleteOrder/${id}`)
  }
}
