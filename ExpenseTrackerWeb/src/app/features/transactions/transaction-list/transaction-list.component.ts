import { Component, OnInit } from '@angular/core';
import { TransactionService } from '../transaction.service';
import { Transaction } from '../transaction.model';
import { FormControl, FormGroup, Validator, Validators } from '@angular/forms';

@Component({
  selector: 'app-transaction-list',
  standalone: false,
  templateUrl: './transaction-list.component.html',
  styleUrls: [
    './transaction-list.component.scss',
    './transaction-list.component.css'
  ]
})
export class TransactionListComponent implements OnInit {

  transactions: Transaction[] = [];

  transactionForm = new FormGroup({
    userAccountId: new FormControl('', Validators.required),
    transactionCategoryId: new FormControl('', Validators.required),
    transactionAmount: new FormControl(0, [Validators.required, Validators.min(1)]),
    note: new FormControl('')
  });

  constructor(private TransactionService: TransactionService) { }

  ngOnInit() {
    this.getTransactions(2);
  }

  getTransactions(userAccountId: number) {
    this.TransactionService.getTransactions(userAccountId).subscribe(res => {
      console.log(res)
      this.transactions = res;
    });
  }

  onSubmit() {
    if (this.transactionForm.invalid) return; 
    console.log(this.transactionForm.value);  
    let formData = this.transactionForm.value

    this.TransactionService.addTransaction(formData).subscribe(res =>{
      console.log(res);
      
      if (res == 0){
        console.error('Error');
      }else{
        this.getTransactions(Number(formData.userAccountId));
        this.transactionForm.reset
      }
    })
  }
}
