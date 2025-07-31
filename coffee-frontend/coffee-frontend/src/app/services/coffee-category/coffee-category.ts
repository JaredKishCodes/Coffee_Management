import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CoffeeCategoryApiResponse } from '../../models/coffee.model';


@Injectable({
  providedIn: 'root'
})
export class CoffeeCategoryService {

  apiUrl = 'https://localhost:7168/api/CoffeeCategory'

  constructor(private http: HttpClient) {
      

  }

  
  getCoffeeCategories() :Observable<CoffeeCategoryApiResponse> {
    return this.http.get<CoffeeCategoryApiResponse>(`${this.apiUrl}/GetAllCoffeeCategories`);
  }
  
}
