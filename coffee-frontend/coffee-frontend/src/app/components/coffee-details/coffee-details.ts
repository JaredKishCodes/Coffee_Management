import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CoffeeItems } from '../coffee-items/coffee-items';
import { Coffee, CoffeeResponse } from '../../models/coffee.model';
import { CoffeeService } from '../../services/coffee/coffee';
import { CartItemService } from '../../services/cart-item/cart-item';
import { Cart, CartItem, CartItemDto } from '../../models/carItem.model';
import { CartService } from '../../services/cart/cart-service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-coffee-details',
  imports: [CommonModule,FormsModule],
  templateUrl: './coffee-details.html',
  styleUrl: './coffee-details.css'
})
export class CoffeeDetails implements OnInit {

  showSuccess : boolean = false;

  coffeeId! : number;
  coffee! : Coffee;
  cartItem! : CartItem;
  cart! : Cart;

  route = inject(ActivatedRoute)
  coffeeService = inject(CoffeeService)
  cartItemService = inject(CartItemService);
  cartService = inject(CartService);

  quantity: number = 1; // Default quantity for adding to cart  

  
  ngOnInit(): void {
    this.coffeeId = +this.route.snapshot.paramMap.get('id')!;
    this.getCoffeeById()
    this.getCartItemById();
  }

  addToCart() {
    const addCartItemDto = {
      coffeeItemId: this.coffeeId,
      quantity: this.quantity, // Default to 1 for simplicity
      cartId: this.cart.id // Assuming you have the cart ID available
    };

    this.cartItemService.addCartItem(addCartItemDto).subscribe({
      next: (res: CartItemDto) => {
        if (res.success) {
          console.log('Cart item added successfully:', res.data);
          this.cartItem = res.data; // Update the local cart item
          this.showSuccess = true;
        } else {
          console.error('Failed to add cart item:', res.message);
        }
        setTimeout(() => {
          this.showSuccess = false;
        }, 2000);
        this.quantity = 1; // Reset quantity after adding to cart
      },
      error: (err) => {
        console.error('Error adding cart item:', err);
      }
    });
  }

  getCartItemById() {
  this.cartItemService.getCartItemById(this.coffeeId).subscribe({
    next: (res) => {
      if (res) {
        this.cartItem = res.data;
        console.log('Cart Item:', res.data);
      } else {
        console.warn('No cart item found for coffeeId:', this.coffeeId);
      }
    },
    error: (err) => {
      console.error('Error fetching cart item:', err);
    }
  });
}



  getCoffeeById(){
    this.coffeeService.getCoffeeById(this.coffeeId).subscribe({
      next: (res) => {
        this.coffee = res.data;
      }
    });
  }}
