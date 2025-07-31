
import { Routes } from '@angular/router';

import { Home } from './components/home/home';
import { Login } from './components/login/login';
import { Menu } from './components/menu/menu';
import { authGuard } from './components/guard/auth-guard';
import { CoffeeItems } from './components/coffee-items/coffee-items';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: Home },
  { path: 'login', component: Login },
  { path: 'menu', component: Menu },
  { path: 'coffees/:id', component:CoffeeItems , canActivate:[authGuard]}
]
