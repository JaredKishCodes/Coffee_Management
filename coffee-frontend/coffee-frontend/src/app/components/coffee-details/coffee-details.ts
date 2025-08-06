import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CoffeeItems } from '../coffee-items/coffee-items';
import { Coffee, CoffeeResponse } from '../../models/coffee.model';
import { CoffeeService } from '../../services/coffee/coffee';

@Component({
  selector: 'app-coffee-details',
  imports: [],
  templateUrl: './coffee-details.html',
  styleUrl: './coffee-details.css'
})
export class CoffeeDetails implements OnInit {

  coffeeId! : number;
  coffee! : Coffee;

  route = inject(ActivatedRoute)
  coffeeService = inject(CoffeeService)


  ngOnInit(): void {
    this.coffeeId = +this.route.snapshot.paramMap.get('id')!;
    this.getCoffeeById()
  }

  getCoffeeById(){
    this.coffeeService.getCoffeeById(this.coffeeId).subscribe({
      next: (res) => {
        this.coffee = res.data;
      }
    });
  }}
