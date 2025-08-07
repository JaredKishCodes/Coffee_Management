import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CoffeeItems } from '../coffee-items/coffee-items';
import { Coffee, CoffeeResponse } from '../../models/coffee.model';
import { CoffeeService } from '../../services/coffee/coffee';
import { CartItemService } from '../../services/cart-item/cart-item';
import { AddCartItemDto, Cart, CartItem, CartItemDto } from '../../models/carItem.model';
import { CartService } from '../../services/cart/cart-service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CartResponse } from '../../models/cart.model';

@Component({
  selector: 'app-coffee-details',
  imports: [CommonModule, FormsModule],
  templateUrl: './coffee-details.html',
  styleUrl: './coffee-details.css'
})
export class CoffeeDetails implements OnInit {

  showSuccess: boolean = false;

  coffeeId!: number;
  coffee!: Coffee;
  cartItem!: CartItem;
  cart!: Cart;

  route = inject(ActivatedRoute)
  coffeeService = inject(CoffeeService)
  cartItemService = inject(CartItemService);
  cartService = inject(CartService);

  quantity: number = 1; // Default quantity for adding to cart  

  ngOnInit(): void {
    this.coffeeId = +this.route.snapshot.paramMap.get('id')!;
    this.getCoffeeById();
  }

  addCartItem() {
    // Create a cart with the item
    const cartItemData: AddCartItemDto = {
      coffeeItemId: this.coffeeId,
      quantity: this.quantity,
      cartId: 0 // This will be set by the backend
    };

    const userString = localStorage.getItem('user');
    const user = userString ? JSON.parse(userString) : null;

    if (!user) {
      console.error('No user info found');
      return;
    }

    const fullName = user.fullName;

    this.cartService.createCart(fullName, cartItemData).subscribe({
      next: (cartResponse: CartResponse) => {
        if (cartResponse.success) {
          const cartId = cartResponse.data.id.toString();
          localStorage.setItem('cartId', cartId);
          this.showSuccess = true;
          console.log('Cart created successfully:', cartResponse.data);
        } else {
          console.error('Failed to create cart:', cartResponse.message);
        }
        setTimeout(() => {
          this.showSuccess = false;
        }, 2000);
        this.quantity = 1; // Reset quantity after adding to cart
      },
      error: (err: any) => {
        console.error('Error creating cart:', err);
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

  getCoffeeById() {
    this.coffeeService.getCoffeeById(this.coffeeId).subscribe({
      next: (res) => {
        this.coffee = res.data;
      }
    });
  }

  increaseQuantity() {
    this.quantity++;
  }

  decreaseQuantity() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }
}


