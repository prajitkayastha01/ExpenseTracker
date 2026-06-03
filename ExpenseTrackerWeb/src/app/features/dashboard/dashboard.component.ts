import { Component, OnInit } from '@angular/core';
import { TransactionService } from './transaction.service';

@Component({
  selector: 'app-dashboard',
  standalone: false,
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {

  balance: number = 0;
  constructor(private transactionService: TransactionService) { }

  ngOnInit() {
    this.getBalance(2); // Example user account ID
  }

  getBalance(userAccountId: number) {
    this.transactionService.getBalance(userAccountId).subscribe(balance => {
      console.log('Current Balance:', balance);
      this.balance = balance;
    });
  }
}
