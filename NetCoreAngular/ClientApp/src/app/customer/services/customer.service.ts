import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Customer, CustomerModel} from "../models/customer";
import {map} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class CustomersService {

  constructor(private http:HttpClient) { } 
  baseUrl:string = `https://localhost:44329/api/customer`

  getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(`${this.baseUrl}`).pipe(map( (customers) => customers))
  }

  getCustomer(empId: string): Observable<Customer> {
    return this.http.get<Customer>(`${this.baseUrl}/${empId}`).pipe(map( (customer) => customer))
  }

  updateCustomer(empId: string | number, changes: Partial<Customer>): Observable<Customer> {
    return this.http.put<Customer>(`${this.baseUrl}/${empId}`, changes );
  }
  createCustomer(reqObj: Customer): Observable<Customer> {
    return this.http.post<Customer>(`${this.baseUrl}`, reqObj );
  }
  deleteCustomer(empId: string): Observable<Customer> {
    return this.http.delete<Customer>(`${this.baseUrl}/${empId}` );
  }
}

 