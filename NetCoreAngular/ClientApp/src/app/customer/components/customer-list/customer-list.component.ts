import { Component, OnInit } from '@angular/core';
import {select, Store} from "@ngrx/store";

import {CustomersService} from "../../services/customer.service";
import {loadCustomers} from "../../store/customer.actions";
import * as CustomerAction from '../../store/customer.actions';
import {Observable} from "rxjs";
import {Customer, CustomerModel} from "../../models/customer";
import { Router } from '@angular/router';
import { CustomerState } from '../../store/customer.reducer';
import { selectCustomers } from '../../store/customer.selectors';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
  customers$: Observable<Customer[]> = this.store.pipe(select(selectCustomers));
  constructor(private store: Store<CustomerState>,
              private router: Router,
              private customersService: CustomersService) { }

  ngOnInit(): void {
    this.store.dispatch(CustomerAction.loadCustomers());

  }


  deleteCustomer(customer) {
    console.log("DEletet ", customer)
    this.store.dispatch(CustomerAction.deleteCustomer({id: customer.id}))
  }

}
