import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { TransactionListComponent } from './features/transactions/transaction-list/transaction-list.component';
import { InvestmentListComponent } from './features/investments/investment-list/investment-list.component';
import { ReportsComponent } from './features/reports/reports/reports.component';
import { LoginComponent } from './features/login/login.component';
import { authGuard } from './shared/guards/auth.guard';
import { RegisterComponent } from './features/register/register.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent},
  { path: 'dashboard',    component: DashboardComponent, canActivate: [authGuard] },
  { path: 'transactions', component: TransactionListComponent, canActivate: [authGuard] },
  { path: 'investments',  component: InvestmentListComponent, canActivate: [authGuard] },
  { path: 'reports',      component: ReportsComponent,canActivate: [authGuard] },
  { path: '**', redirectTo: '/login'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }