import { Component, OnInit } from '@angular/core';
import { UserInvestmentDto } from '../user-investment.model';
import { UserInvestmentService } from '../user-investment.service';
import { InvestmentType } from '../investment-type.model';
import { InvestmentTypeService } from '../investment-type.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-investment-list',
  standalone: false,
  templateUrl: './investment-list.component.html',
  styleUrl: './investment-list.component.scss'
})
export class InvestmentListComponent implements OnInit{

  investments: UserInvestmentDto[] = [];
  userId: number = 1;
  investmentTypes: InvestmentType[] = [];

  investmentForm = new FormGroup({
    investmentTypeId: new FormControl(0,Validators.required),
    symbol: new FormControl('', [Validators.required]),
    quantity: new FormControl(0,[Validators.required, Validators.min(1)]),
    buyPrice: new FormControl(0,[Validators.required, Validators.min(1)]),
    purchaseDate: new FormControl('', Validators.required)
  });

  constructor(private uis: UserInvestmentService, private it: InvestmentTypeService){}

  ngOnInit(): void {
    this.loadInvestments();
    this.getAllInvestmentTypes();
  }

  loadInvestments(){
    this.uis.getUserInvestmentsByUserId(this.userId).subscribe(res =>{
      this.investments = res;
    })
  }

  getAllInvestmentTypes(){
    this.it.getAllInvestmentTypes().subscribe(res => {
      this.investmentTypes = res;
    })
  }


  onSubmit(){

    console.log(typeof(this.investmentForm.value.investmentTypeId))
    console.log(typeof(this.investmentForm.value.symbol))
    console.log(typeof(this.investmentForm.value.quantity))
    console.log(typeof(this.investmentForm.value.buyPrice))
    console.log(typeof(this.investmentForm.value.purchaseDate))
  }
}
