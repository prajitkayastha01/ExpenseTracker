import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { TransactionListComponent } from './features/transactions/transaction-list/transaction-list.component';
import { InvestmentListComponent } from './features/investments/investment-list/investment-list.component';
import { ReportsComponent } from './features/reports/reports/reports.component';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { ReactiveFormsModule} from '@angular/forms';
import { LoginComponent } from './features/login/login.component'

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    TransactionListComponent,
    InvestmentListComponent,
    ReportsComponent,
    NavbarComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    MatToolbarModule,
    ReactiveFormsModule,
    MatButtonModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }