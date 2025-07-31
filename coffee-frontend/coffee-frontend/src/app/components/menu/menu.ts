import { Component, inject, OnInit } from '@angular/core';
import { CoffeeCategoryService } from '../../services/coffee-category/coffee-category';
import { CommonModule } from '@angular/common';
import { CoffeeCategoryApiResponse } from '../../models/coffee.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './menu.html',
  styleUrls: ['./menu.css'] // ← corrected typo (was `styleUrl`)
})
export class Menu implements OnInit {
  private router = inject(Router);
  private coffeeCategoriesService = inject(CoffeeCategoryService);

  coffeeCategories: any[] = [];

  ngOnInit(): void {
    this.getAllCoffeeCategories();
  }

  goToCategory(categoryId: number) {
    this.router.navigate(['/categories', categoryId]);
  }

  private getAllCoffeeCategories() {
    this.coffeeCategoriesService.getCoffeeCategories().subscribe({
      next: (response: CoffeeCategoryApiResponse) => {
        this.coffeeCategories = response.data;
      },
      error: (error) => {
        console.error('Error fetching coffee categories:', error);
      }
    });
  }

  goToCoffeeItem(){
    const token = localStorage.getItem('token');
    if (token) {
      this.router.navigate(['/coffee-items']);
    }
    else{
      this.router.navigate(['/login']);
    }
  }
}
