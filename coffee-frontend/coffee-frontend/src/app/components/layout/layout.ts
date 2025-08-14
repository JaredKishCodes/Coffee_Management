import { Component, inject } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { IAuthResponse } from '../../models/auth.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-layout',
  imports: [RouterOutlet, RouterModule,CommonModule],
  templateUrl: './layout.html',
  styleUrl: './layout.css'
})
export class Layout {

  router = inject(Router)
  user! :IAuthResponse;


  onLogout(){
    localStorage.clear(); // clears all localStorage data
  this.router.navigate(['/login']);

  }
}
