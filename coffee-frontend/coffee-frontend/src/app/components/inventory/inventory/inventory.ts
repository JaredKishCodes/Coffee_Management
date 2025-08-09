import { Component } from '@angular/core';
import { CoffeeInventoryDto } from '../../../models/inventory.model';

@Component({
  selector: 'app-inventory',
  imports: [],
  templateUrl: './inventory.html',
  styleUrl: './inventory.css'
})
export class Inventory {
  coffeeInventory : CoffeeInventoryDto[] = []
}
