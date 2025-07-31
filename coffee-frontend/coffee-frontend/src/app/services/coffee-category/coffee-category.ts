import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CoffeeCategory {

  apiUrl = 'https://localhost:7168/api/CoffeeCategory'
  constructor() {
    // Initialization logic can go here
  }

  // Example method to fetch coffee categories
  getCategories() {
    // This would typically make an HTTP request to fetch categories
    return ['Espresso', 'Latte', 'Cappuccino', 'Americano'];
  }
  
}
