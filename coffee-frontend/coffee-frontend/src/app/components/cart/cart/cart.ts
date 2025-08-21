import { Component, inject, OnInit } from '@angular/core';
import { CartService } from '../../../services/cart/cart-service';
import { CartDto } from '../../../models/cart.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CartItemService } from '../../../services/cart-item/cart-item';
import { OrderService } from '../../../services/order/order-service';
import { AuthService } from '../../../services/auth/auth';
import { CreateOrderItemDto, OrderDto } from '../../../models/order.model';
import Swal from 'sweetalert2';
import { reduce } from 'rxjs';


@Component({
  selector: 'app-cart',
  imports: [CommonModule, FormsModule],
  templateUrl: './cart.html',
  styleUrl: './cart.css'
})
export class CartComponent implements OnInit {
  authService = inject(AuthService);
  cartService = inject(CartService);
  cartItemService = inject(CartItemService);
  orderService = inject(OrderService);

  showCheckoutSuccess: boolean = false;

  orders : OrderDto[] = []

  cart: CartDto = {
    id: 0,
    customerName: '',
    totalPrice: 0,
    orderDate: '',
    orderStatus: 'Pending',
    cartItems: []
  };

  ngOnInit(): void {
    this.getCartById();
    this.getOrder();
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

  removeItem(id: number) {
    Swal.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes, delete it!"
    }).then((result) => {
      if (result.isConfirmed) {
        Swal.fire({
          title: "Deleted!",
          text: "Coffee Item has been deleted.",
          icon: "success"
        });

         this.cartItemService.removeCartItem(id).subscribe({
        next: (res) => {
          this.getCartById();
          console.log(res);
        },
        error: err => {
          console.error(err)
        }
      })
      }
     
    });

  }

  updateTotal() {
    this.cart.totalPrice = this.cart.cartItems
      .reduce((sum, item) => sum + (item.unitPrice * item.quantity), 0);
  }

  addOrder() {
    const auth = localStorage.getItem('auth');
    if (auth) {
      var user = JSON.parse(auth)
    }
    const orderRequest = {
      customerName: user.fullName,
      orderItems: this.cart.cartItems.map(item => ({
        coffeeItemId: item.coffeeItemId,
        quantity: item.quantity,
      })),
    };
    console.log(orderRequest);
    this.orderService.addOrder(orderRequest).subscribe({

      next: (res) => {
        console.log('Order placed:', res)
        localStorage.setItem('orderId',res.data.id.toString());
        this.getOrder();

        Swal.fire({
          title: "Thankyou!",
          text: "Your order has been placed successfully!",
          icon: "success"
        });
      },
      error: err => console.error('Error placing order:', err),
    })

  }

  getOrder(){
    const orderIdString = localStorage.getItem('orderId');
    const orderId = orderIdString ? Number(orderIdString) : null;

      if (!orderId) {
    console.error('No orderId found in localStorage');
    return;
    }
    this.orderService.getOrderByid(orderId).subscribe({
    next: (res) => {
      console.log('Order details:', res);
      // you can store it in a property and bind to UI
      this.orders = [res.data];
    },
    error: (err) => console.error('Error fetching order:', err),
  });
  }
}
