import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [ RouterModule,CommonModule],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {

  
}
