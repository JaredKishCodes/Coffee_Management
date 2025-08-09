import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../models/order.model';
import { Inventory } from '../../components/inventory/inventory/inventory';
import { CoffeeInventoryDto } from '../../models/inventory.model';

@Injectable({
  providedIn: 'root'
})
export class InventoryService {

  http = inject(HttpClient)

  apiUrl = 'https://localhost:7168/api/Inventory';

  getAllInventory() :Observable<ApiResponse<CoffeeInventoryDto[]>>{
   return this.http.get<ApiResponse<CoffeeInventoryDto[]>>(`${this.apiUrl}/GetAllInventory`);
  }
}
