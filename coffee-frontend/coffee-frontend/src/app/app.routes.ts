
import { Routes } from '@angular/router';

import { Home } from './components/home/home';
import { Login } from './components/login/login';
import { Menu } from './components/menu/menu';
import { authGuard } from './guard/auth-guard';
import { CoffeeItems } from './components/coffee-items/coffee-items';
import { CoffeeDetails } from './components/coffee-details/coffee-details';
import { CartComponent } from './components/cart/cart/cart';
import { Order } from './components/order/order';
import { Inventory } from './components/inventory/inventory/inventory';
import { adminGuard } from './guard/admin-guard';
import { NotFound } from './components/not-found/not-found';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: Home },
  { path: 'login', component: Login },
  { path: 'menu', component: Menu },
  { path: 'coffees/:categoryId', component:CoffeeItems , canActivate:[authGuard]},
  {path : 'coffee/:id', component:CoffeeDetails},
  {path : 'cart', component: CartComponent},
  {path : 'orders', component:Order, canActivate:[adminGuard]},
  {path: 'inventory', component:Inventory, canActivate:[adminGuard]},
  {path: '**', component:NotFound}

]
