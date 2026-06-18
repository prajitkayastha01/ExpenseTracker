import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserInvestmentDto } from './user-investment.model';

@Injectable({
  providedIn: 'root'
})
export class UserInvestmentService {
  private baseUrl = 'http://localhost:5081/api/UserInvestments';

  constructor(private http: HttpClient) { }

  getUserInvestmentsByUserId(userId: number): Observable<UserInvestmentDto[]>{
    return this.http.get<UserInvestmentDto[]>(`${this.baseUrl}/${userId}`)
  }
}
