import { Component, inject } from '@angular/core';
import { CoffeeInventoryDto } from '../../../models/inventory.model';
import { InventoryService } from '../../../services/inventory/inventory-service';

@Component({
  selector: 'app-inventory',
  imports: [],
  templateUrl: './inventory.html',
  styleUrl: './inventory.css'
})
export class Inventory {

  inventoryService = inject(InventoryService)

  coffeeInventory : CoffeeInventoryDto[] = []
  

  getAllInventory(){
    this.inventoryService.getAllInventory().subscribe({
      next:(res)=>{
        this.coffeeInventory = res.data
      }
    })
  }

}
