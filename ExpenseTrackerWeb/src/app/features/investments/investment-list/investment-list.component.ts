import { Component, OnInit } from '@angular/core';
import { UserInvestmentDto } from '../user-investment.model';
import { UserInvestmentService } from '../user-investment.service';

@Component({
  selector: 'app-investment-list',
  standalone: false,
  templateUrl: './investment-list.component.html',
  styleUrl: './investment-list.component.scss'
})
export class InvestmentListComponent implements OnInit{

  investments: UserInvestmentDto[] = [];
  userId: number = 1;

  constructor(private investmentService: UserInvestmentService){}

  ngOnInit(): void {
    this.loadInvestments();
  }

  loadInvestments(){
    this.investmentService.getUserInvestmentsByUserId(this.userId).subscribe(res =>{
      this.investments = res;
    })
  }

}
