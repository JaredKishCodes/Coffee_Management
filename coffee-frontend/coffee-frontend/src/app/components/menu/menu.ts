import { Component, inject, OnInit } from '@angular/core';
import { CoffeeCategoryService } from '../../services/coffee-category/coffee-category';
import { CommonModule } from '@angular/common';
import { CoffeeCategoryApiResponse } from '../../models/coffee.model';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './menu.html',
  styleUrls: ['./menu.css'] // ← corrected typo (was `styleUrl`)
})
export class Menu implements OnInit {
  private router = inject(Router);
   private route = inject(ActivatedRoute); 
  private coffeeCategoriesService = inject(CoffeeCategoryService);

  coffeeCategories: any[] = [];

  ngOnInit(): void {
   
    this.getAllCoffeeCategories();
    const categoryId = this.route.snapshot.paramMap.get('categoryId');
    if (categoryId) {
      this.getCoffeesByCategory(+categoryId);
    }
  }

  goToCategory(categoryId: number) {
    this.router.navigate(['/categories', categoryId]);
  }

   getAllCoffeeCategories() {
    this.coffeeCategoriesService.getCoffeeCategories().subscribe({
      next: (response: CoffeeCategoryApiResponse) => {
        this.coffeeCategories = response.data;
      },
      error: (error) => {
        console.error('Error fetching coffee categories:', error);
      }
    });
  }

 goToCoffeeItem(catId: number | undefined) {
  if (!catId) {
    console.error('Category ID is undefined');
    return;
  }
    const authData = localStorage.getItem('auth');
    let token : string | null = null;
    if (authData) {
      try {
        token = JSON.parse(authData).token;
      } catch {
        token = null;
      }
    }
    if (token) {
      this.router.navigate(['/coffees', catId]);
    }
    else{
      this.router.navigate(['/login']);
    }
  }
  getCoffeesByCategory(categoryId: number){
    this.coffeeCategoriesService.getCoffeesByCategory(categoryId).subscribe({
      next: (response: CoffeeCategoryApiResponse) => {
        this.coffeeCategories = response.data;
      },
      error: (error) => {
        console.error('Error fetching coffee categories:', error);
      }
    });
  }

  
}
