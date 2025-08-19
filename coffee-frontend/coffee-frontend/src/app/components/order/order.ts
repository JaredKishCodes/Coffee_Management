import { Component, inject, OnInit } from '@angular/core';
import { OrderDto, OrderStatus } from '../../models/order.model';
import { OrderService } from '../../services/order/order-service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CoffeeRequest } from '../../models/coffee.model';

@Component({
  selector: 'app-order',
  imports: [CommonModule, FormsModule],
  templateUrl: './order.html',
  styleUrl: './order.css'
})
export class Order implements OnInit {

  orderService = inject(OrderService)

  orders: OrderDto[] = []
  OrderStatus = OrderStatus;
  coffeeId!: number;

  coffeeObj: CoffeeRequest = {
    name: '',
    description: '',
    price: 0,
    size: 'Small',
    stock: 0,
    isAvailable: true,
    imageUrl: '',
    categoryId: 0
  };

  orderObj: OrderDto = {
    id: 0,
    customerName: '',
    totalPrice: 0,
    orderDate: new Date().toISOString(), // default to now
    orderStatus: OrderStatus.Pending,   // or whatever default you need
    orderItems: [] // 👈 start empty, populate later
  };


  ngOnInit(): void {
    this.getAllOrders()
  }

  getAllOrders() {
    this.orderService.getAllOrders().subscribe({
      next: (response) => {
        console.log(response)
        this.orders = response.data
      },
      error: () => {
        console.log("error fetching orders")
      }
    })
  }

  openEditModal(order: OrderDto) {
  this.orderObj = {
    id: order.id,
    customerName: order.customerName,
    totalPrice: order.totalPrice,
    orderDate: order.orderDate,
    orderStatus: order.orderStatus,
    orderItems: order.orderItems,
  };

  (document.getElementById("my_modal_2") as HTMLDialogElement).showModal();
}



  updateOrder() {
  const updateOrderDto = {
    customerName: this.orderObj.customerName,
    totalPrice: this.orderObj.totalPrice,
    orderDate: this.orderObj.orderDate,
    orderStatus: this.orderObj.orderStatus,
    orderItems: this.orderObj.orderItems
  };

  this.orderService.updateOrder(this.orderObj.id, updateOrderDto).subscribe({
    next: (res) => {
      console.log("Order updated", res);
    },
    error: (err) => {
      console.error("Update failed", err);
    }
  });
}


  deleteOrder(orderId: number) {
    const confirmDelete = confirm("Are you sure you want to delete this order?");
    if (!confirmDelete) {
      return; // user canceled
    }

    this.orderService.deleteOrder(orderId).subscribe({
      next: (res) => {
        console.log("Order deleted:", res);
        this.getAllOrders(); // refresh orders
      },
      error: (err) => {
        console.error("Error deleting order:", err);
      }
    });
  }


}
