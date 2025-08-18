import { Component, inject, OnInit } from '@angular/core';
import { OrderDto, OrderStatus } from '../../models/order.model';
import { OrderService } from '../../services/order/order-service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CoffeeRequest } from '../../models/coffee.model';

@Component({
  selector: 'app-order',
  imports: [CommonModule,FormsModule],
  templateUrl: './order.html',
  styleUrl: './order.css'
})
export class Order implements OnInit {

    orderService = inject(OrderService)

    orders :OrderDto[] = []
    OrderStatus = OrderStatus;
    coffeeId! : number;

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

    ngOnInit(): void {
      this.getAllOrders()
    }

    getAllOrders(){
      this.orderService.getAllOrders().subscribe({
        next:(response) =>{
          console.log(response)
          this.orders = response.data
        },
        error:()=>{
          console.log("error fetching orders")
        }
      })
    }

      updateOrder(orderId:number){
        this.orderService.updateOrder(orderId).subscribe({
          next:(res)=>{
            console.log(res);
          }
        })
      }

}
