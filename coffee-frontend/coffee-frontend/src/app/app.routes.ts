import { Routes } from '@angular/router';
import { Layout } from './components/layout/layout';
import { Login } from './components/login/login';
import { Home } from './components/home/home';
import { Menu } from './components/menu/menu';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: Home },
  { path: 'login', component: Login } ,
  {path:'menu',component:Menu},
];

