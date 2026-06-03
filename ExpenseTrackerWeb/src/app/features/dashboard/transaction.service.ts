import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private baseUrl = 'http://localhost:5081/api/Transaction';
  constructor(private http: HttpClient) { }

  getBalance(userAccountId: number): Observable<number> {
    var response = this.http.get<number>(`${this.baseUrl}/balance/${userAccountId}`);
    return response;
  }
}
