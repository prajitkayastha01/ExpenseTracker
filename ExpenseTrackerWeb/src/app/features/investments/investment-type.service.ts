import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { InvestmentType } from "./investment-type.model";

@Injectable({
  providedIn: 'root'
})
export class InvestmentTypeService {
    private baseUrl = 'http://localhost:5081/api/InvestmentType';

  constructor(private http: HttpClient) { }

  getAllInvestmentTypes(): Observable<InvestmentType[]>{
    return this.http.get<InvestmentType[]>(`${this.baseUrl}`)
  }

}
