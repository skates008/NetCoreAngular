import {Component, OnInit} from '@angular/core';
import {select, Store} from '@ngrx/store';
import * as customerActions from '../state/customer.action';
import * as fromCustomer from '../state/customer.reducer';
import {Customer} from '../customer.model';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
  customers$: Observable<Customer[]>;
  // tslint:disable-next-line:ban-types
  error$: Observable<String>;

  constructor(private store: Store<any>) {
  }

  ngOnInit() {
    this.store.dispatch(new customerActions.LoadCustomers());
    this.customers$ = this.store.pipe(select(fromCustomer.getCustomers));
    this.error$ = this.store.pipe(select(fromCustomer.getError));
  }

  deleteCustomer(customer: Customer) {
    if (confirm('Are You Sure You want to Delete the User?')) {
      this.store.dispatch(new customerActions.DeleteCustomer(customer.id));
    }
  }

  editCustomer(customer: Customer) {
    debugger
    this.store.dispatch(new customerActions.LoadCustomer(customer.id));
  }
}
