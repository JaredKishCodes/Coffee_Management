
import { Routes } from '@angular/router';

import { Home } from './components/home/home';
import { Login } from './components/login/login';
import { Menu } from './components/menu/menu';
import { authGuard } from './components/guard/auth-guard';
import { CoffeeItems } from './components/coffee-items/coffee-items';
import { CoffeeDetails } from './components/coffee-details/coffee-details';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: Home },
  { path: 'login', component: Login },
  { path: 'menu', component: Menu },
  { path: 'coffees/:categoryId', component:CoffeeItems , canActivate:[authGuard]},
  {path : 'coffee/:id', component:CoffeeDetails}

]
