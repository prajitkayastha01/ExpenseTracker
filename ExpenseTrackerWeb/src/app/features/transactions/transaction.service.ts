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

  getTransactions(): Observable<Transaction[]>{
    const response = this.http.get<Transaction[]>(`${this.baseUrl}/transactions`);

    return response;
  }

  addTransaction(json: any): Observable<any>{
    return  this.http.post<any>(`${this.baseUrl}`, json);
  }

  deleteTransaction(id: number): Observable<any>{
    const response = this .http.delete<any>(`${this.baseUrl}/${id}`);
    return response;
  }
}
