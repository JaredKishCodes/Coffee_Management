import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Coffee, CoffeeResponse } from '../../models/coffee.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CoffeeService {

  constructor(private http : HttpClient) { }

  apiUrl = 'https://localhost:7168/api/Coffee'
  // Example method to get coffee details by ID
  getCoffeeById(id: number): Observable<CoffeeResponse> {
    return this.http.get<CoffeeResponse>(`${this.apiUrl}/${id}`);
  }
  
}


