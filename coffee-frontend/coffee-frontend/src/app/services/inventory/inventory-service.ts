import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/order.model';
import { Inventory } from '../../components/inventory/inventory/inventory';
import { CoffeeInventoryDto } from '../../models/inventory.model';
import { CoffeeRequest, CoffeeResponse } from '../../models/coffee.model';

@Injectable({
  providedIn: 'root'
})
export class InventoryService {

  http = inject(HttpClient)

  apiUrl = 'https://localhost:7168/api/Inventory';
  coffeeUrl = 'https://localhost:7168/api/Coffee'

  getAllInventory() :Observable<ApiResponse<CoffeeInventoryDto[]>>{
   return this.http.get<ApiResponse<CoffeeInventoryDto[]>>(`${this.apiUrl}/GetAllInventory`);
  }

  addCoffee(coffeeReq:CoffeeRequest):Observable<ApiResponse<CoffeeResponse>>{
    return this.http.post<ApiResponse<CoffeeResponse>>(`${this.coffeeUrl}`,coffeeReq)
  }
}
