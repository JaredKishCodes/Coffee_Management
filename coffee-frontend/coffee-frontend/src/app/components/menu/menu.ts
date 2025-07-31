import { Component, inject, OnInit } from '@angular/core';
import { CoffeeCategoryService } from '../../services/coffee-category/coffee-category';
import { CommonModule } from '@angular/common';
import { CoffeeCategoryApiResponse } from '../../models/coffee.model';

@Component({
  selector: 'app-menu',
  imports: [CommonModule],
  templateUrl: './menu.html',
  styleUrl: './menu.css'
})
export class Menu implements OnInit {

  coffeeCategories: any[] = [];

  ngOnInit(): void {
    this.getAllCoffeeCategories();
  }

  goToCoffeeItem() {
    // Logic to navigate to coffee item details
  }
  coffeeCategoriesService = inject(CoffeeCategoryService)

  getAllCoffeeCategories() {
    this.coffeeCategoriesService.getCoffeeCategories().subscribe({
      next: (response : CoffeeCategoryApiResponse) => {
        this.coffeeCategories = response.data;
      },
      error: (error) => {
        console.error('Error fetching coffee categories:', error);
      }
    });
  }
  
}
