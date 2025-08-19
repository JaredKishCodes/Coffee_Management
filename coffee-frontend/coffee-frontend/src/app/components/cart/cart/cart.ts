import { Component, inject, OnInit } from '@angular/core';
import { CartService } from '../../../services/cart/cart-service';
import { CartDto } from '../../../models/cart.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CartItemService } from '../../../services/cart-item/cart-item';
import { OrderService } from '../../../services/order/order-service';
import { AuthService } from '../../../services/auth/auth';
import { CreateOrderItemDto } from '../../../models/order.model';

@Component({
  selector: 'app-cart',
  imports: [CommonModule,FormsModule],
  templateUrl: './cart.html',
  styleUrl: './cart.css'
})
export class CartComponent implements OnInit {
  authService = inject(AuthService);
  cartService = inject(CartService);
  cartItemService = inject(CartItemService);
  orderService = inject(OrderService);

  showCheckoutSuccess : boolean = false;

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

  removeItem(id:number){
     const remove = confirm("Remove item?")
        if(!remove){
          return;
        }        
    this.cartItemService.removeCartItem(id).subscribe({
      next:(res)=>{
          this.getCartById();
          console.log(res);
      },
      error:err=>{
        console.error(err)
      }
    })
  }

  updateTotal() {
  this.cart.totalPrice = this.cart.cartItems
    .reduce((sum, item) => sum + (item.unitPrice * item.quantity), 0);
}

  addOrder(){
    const auth = localStorage.getItem('auth');
    if(auth){
      var user = JSON.parse(auth)
    }
    const orderRequest = {
      customerName : user.fullName,
      orderItems: this.cart.cartItems.map(item =>({
        coffeeItemId: item.coffeeItemId,
        quantity: item.quantity,
      })),
    };
    console.log(orderRequest);
    this.orderService.addOrder(orderRequest).subscribe({
      
      next: (res) =>{console.log('Order placed:', res)
        this.showCheckoutSuccess = true
        setTimeout(()=>{
          this.showCheckoutSuccess = false
        },3000);
      },
      error: err => console.error('Error placing order:', err),
    })
  
}
}
