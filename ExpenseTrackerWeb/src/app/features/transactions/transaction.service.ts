import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Transaction } from './transaction.model';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  private baseUrl = 'http://localhost:5081/api/Transaction'; 
  constructor(private http: HttpClient) { }

  getTransactions(userAccountId: number): Observable<Transaction[]>{
    const response = this.http.get<Transaction[]>(`${this.baseUrl}/${userAccountId}`);

    return response;
  }

  addTransaction(json: any): Observable<any>{
    return  this.http.post<any>(`${this.baseUrl}`, json);
  }
}
