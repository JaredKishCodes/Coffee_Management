import { Component, inject, OnInit } from '@angular/core';
import { CoffeeInventoryDto } from '../../../models/inventory.model';
import { InventoryService } from '../../../services/inventory/inventory-service';
import { CoffeeRequest, CoffeeResponse } from '../../../models/coffee.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-inventory',
  imports: [FormsModule],
  templateUrl: './inventory.html',
  styleUrl: './inventory.css'
})
export class Inventory implements OnInit{

  addedCoffee?: CoffeeResponse;
  
  ngOnInit(): void {
    this.getAllInventory()
  }

  inventoryService = inject(InventoryService)

  coffeeInventory : CoffeeInventoryDto[] = []
  
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



  addCoffee(){
    this.inventoryService.addCoffee(this.coffeeObj).subscribe({
      next:(res)=>{
        this.addedCoffee = res.data
        this.resetCoffeeObj();
      },
      error:err =>{
        console.log(err)
      }  
    })
    
  }

  getAllInventory(){
    this.inventoryService.getAllInventory().subscribe({
      next:(res)=>{
        this.coffeeInventory = res.data
      }
    })
  }

  resetCoffeeObj() {
  this.coffeeObj = {
    name: '',
    description: '',
    price: 0,
    size: 'Small',
    stock: 0,
    isAvailable: true,
    imageUrl: '',
    categoryId: 0
  };
}


}
