import { Component, inject, OnInit, Signal } from '@angular/core';
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
  stock:number = 0;
  selectedInventoryId: number | null = null;
  
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

  openStockModal(inventory: CoffeeInventoryDto) {
    this.selectedInventoryId = inventory.id;
    this.stock = inventory.stock;
    (document.getElementById('my_modal_3') as HTMLDialogElement).showModal();
  }

  updateStock(){
    if (this.selectedInventoryId == null) return;
    this.inventoryService.updateStock(this.selectedInventoryId, { stock: this.stock }).subscribe({
      next:(res)=>{
        console.log(res.data);
        this.getAllInventory();
      }
    })
  }   


}
