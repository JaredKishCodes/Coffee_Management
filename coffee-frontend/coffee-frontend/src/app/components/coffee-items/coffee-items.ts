import { Component, inject } from '@angular/core';
import { OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CoffeeCategoryService } from '../../services/coffee-category/coffee-category';
import { Coffee, CoffeeCategoryApiResponse, CoffeesApiResponse } from '../../models/coffee.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CartItem } from '../../models/carItem.model';
import { CartItemService } from '../../services/cart-item/cart-item';

@Component({
  selector: 'app-coffee-items',
  imports: [CommonModule, FormsModule],
  standalone: true, // optional if using standalone components
  templateUrl: './coffee-items.html',
  styleUrls: ['./coffee-items.css']
})
export class CoffeeItems implements OnInit {
  private route = inject(ActivatedRoute);
  private coffeeService = inject(CoffeeCategoryService);
  private cartItemService = inject(CartItemService);

  coffeeItems: Coffee[] = [];
  cartItems: CartItem[] = [];
  categoryId!: number;

  ngOnInit(): void {
    const idFromRoute = this.route.snapshot.paramMap.get('categoryId');
    console.log('Category ID from URL:', idFromRoute);

    if (idFromRoute) {
      this.categoryId = +idFromRoute; // Convert to number
      this.getCoffeesByCategory();    // ✅ Call the method here
    }
  }
 


  getCoffeesByCategory(): void {
  this.coffeeService.getCoffeesByCategory(this.categoryId).subscribe({
    next: (res: CoffeesApiResponse) => {
      this.coffeeItems = res.data; // or whatever the actual property name is
    },
    error: (err) => {
      console.error('Error fetching coffees:', err);
    }
  });
}

}
