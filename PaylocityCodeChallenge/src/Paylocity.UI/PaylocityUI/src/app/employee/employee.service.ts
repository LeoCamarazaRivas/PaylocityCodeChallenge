import { HttpClient, HttpParams } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { map, tap } from 'rxjs/operators';

import { Employee } from './employee';
import { Dependent } from '../shared/dependent.model';

@Injectable({providedIn:'root'})
export class EmployeeService {
  public employees: Employee[];

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
    ){}

  post<Employee>(employee: Employee): Observable<Employee> {
    let url = `${this.baseUrl}api/deduction`;
    return this.http.post<Employee>(url, employee);
  }

  put<Employee>(id: number, employee: Employee): Observable<Employee> {
    let url = `${this.baseUrl}api/deduction/${id}`;
    return this.http.put<Employee>(url, employee);
  }

  getEmployeeById(id: number) {
    let url = `${this.baseUrl}api/deduction/${id}`;
    return this.http.get<Employee>(url);
  }

  getEmployees() {
    let url = `${this.baseUrl}api/deduction`;
    //let url = 'https://paylocitycodingchallenge-default-rtdb.firebaseio.com/employee.json';
    return this.http.get<Employee[]>(url);

  }
  
}
