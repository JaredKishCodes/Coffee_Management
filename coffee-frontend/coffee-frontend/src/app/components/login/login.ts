import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { AuthService } from '../../services/auth/auth';
import { IAuthResponse, ILogin, IRegister } from '../../models/auth.model';
import { FormsModule } from '@angular/forms';
import { Token } from '@angular/compiler';
import { Router, RouterLink } from '@angular/router';
@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  showLogin: boolean = false;
  showSuccess: boolean = false;
  showLoginSuccess: boolean = false;
  showLoginError: boolean = false

  authService = inject(AuthService)
  router = inject(Router);

  registerObj: IRegister = {
    fullName: '',
    email: '',
    password: '',
    confirmPassword: ''
  }

  loginObj: ILogin = {
    email: '',
    password: ''
  }
  backToHome() {
    this.router.navigateByUrl('/home');
  }

  onLogin() {
    console.log('Login attempt with:', this.loginObj);
    this.authService.login(this.loginObj).subscribe({
      next: (response: IAuthResponse) => {
        localStorage.setItem('cartId',response.cartId);
        console.log('Login response:', response);
        console.log('Setting showLoginSuccess to true');
        this.showLoginSuccess = true;
        this.authService.setUser({
          email:response.email,
          role:response.role
        })
        

        setTimeout(() => {
          console.log('Hiding login success alert');
          this.showLoginSuccess = false;
          this.router.navigateByUrl('/menu');
        }, 2000); // Increased from 2000 to 4000ms
      },
      error: (error) => {
        console.log('Login error, setting showLoginError to true');
        this.showLoginError = true;

        setTimeout(() => {
          console.log('Hiding login error alert');
          this.showLoginError = false;
        }, 4000); // Increased from 2000 to 4000ms
        console.error('Login failed', error);
      }
    });
  }

  onRegister() {
    this.authService.register(this.registerObj).subscribe({
      next: (response : IAuthResponse) => {
        console.log('Registration successful', response);

      localStorage.setItem('auth', JSON.stringify(response));

        this.showSuccess = true;
        this.showLogin = true;


        setTimeout(() => {
          this.showSuccess = false;
        }, 3000);
        this.registerObj = {
          fullName: '',
          email: '',
          password: '',
          confirmPassword: ''
        };
      },
      error: (error) => {

        console.error('Registration failed', error);
      }
    });
  }
}
