import { Component, inject } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-layout',
  imports: [RouterOutlet, RouterModule],
  templateUrl: './layout.html',
  styleUrl: './layout.css'
})
export class Layout {

  router = inject(Router)
  onLogout(){
    localStorage.clear(); // clears all localStorage data
  this.router.navigate(['/login']);
  
  }
}
