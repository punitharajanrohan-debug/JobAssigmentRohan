import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login';
import { PurchaseBillComponent } from './pages/purchase-bill/purchase-bill';

export const routes: Routes = [
 { path: 'login', component: LoginComponent },
 { path: 'purchase-bill', component: PurchaseBillComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' }
];
