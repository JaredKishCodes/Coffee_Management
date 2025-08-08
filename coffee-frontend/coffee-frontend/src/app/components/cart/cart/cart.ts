import { Component, inject, OnInit } from '@angular/core';
import { CartService } from '../../../services/cart/cart-service';
import { CartDto } from '../../../models/cart.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart',
  imports: [CommonModule],
  templateUrl: './cart.html',
  styleUrl: './cart.css'
})
export class CartComponent implements OnInit {
  cartService = inject(CartService);

  cart: CartDto = {
    id: 0,
    customerName: 'jarwd',
    totalPrice: 0,
    orderDate: '',
    orderStatus: 'Pending',
    cartItems: []
  };

  ngOnInit(): void {
    this.getCartById();
  }

  getCartById() {
    const cartIdString = localStorage.getItem('cartId');
    const cartId = cartIdString ? Number(cartIdString) : null;

    if (!cartId) {
      console.error('No cartId found in localStorage');
      return;
    }

    console.log(cartId);
    this.cartService.getCartById(cartId).subscribe({
      next: (res: any) => {
        console.log('Cart response from API:', res);
        this.cart = res.data;
      }
    });
  }
}
