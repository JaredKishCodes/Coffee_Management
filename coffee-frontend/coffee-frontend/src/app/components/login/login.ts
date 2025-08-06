import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Auth } from '../../services/auth/auth';
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

  authService = inject(Auth)
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
    this.authService.login(this.loginObj).subscribe({
      next: (response: IAuthResponse) => {
        console.log('Login successful', response);
        this.showLoginSuccess = true;
        localStorage.setItem('token', response.token);

        setTimeout(() => {
          this.showLoginSuccess = false;
          this.router.navigateByUrl('/menu');
        }, 2000);
      },
      error: (error) => {
        this.showLoginError = true

        setTimeout(() => {
          this.showLoginError = false;
        }, 2000);
        console.error('Login failed', error);
      }
    });
  }

  onRegister() {
    this.authService.register(this.registerObj).subscribe({
      next: (response) => {
        console.log('Registration successful', response);
        this.showSuccess = true;
        this.showLogin = true;
        this.router.navigateByUrl('/menu');

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
