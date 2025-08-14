import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { IAuthResponse } from '../../models/auth.model';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth/auth';

@Component({
  selector: 'app-layout',
  imports: [RouterOutlet, RouterModule,CommonModule],
  templateUrl: './layout.html',
  styleUrl: './layout.css'
})
export class Layout implements OnInit {

  router = inject(Router)
  user! :IAuthResponse;
  isLoggedIn = true;

  authService = inject(AuthService)
  userObj: { email: string } = { email: 'dadad' };


  

  ngOnInit(): void {
    this.authService.isLoggedIn$.subscribe((status)=>{
      this.isLoggedIn = status;
    })
    console.log(this.userObj.email)
  }
  


  onLogout(){
    const logout = confirm("Are you sure you want to  logout?")
    if(logout){
      localStorage.clear(); // clears all localStorage data
      this.authService['loggedIn'].next(false)
      this.router.navigate(['/login']);
      this.isLoggedIn = false;

    }
    

  }
}
