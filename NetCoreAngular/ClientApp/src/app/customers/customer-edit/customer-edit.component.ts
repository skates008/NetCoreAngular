import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';

import { Observable } from 'rxjs';

import * as customerActions from '../state/customer.action';
import * as fromCustomer from '../state/customer.reducer';
import { Customer } from '../customer.model';

@Component({
  selector: 'app-customer-edit',
  templateUrl: './customer-edit.component.html',
  styleUrls: ['./customer-edit.component.css']
})
export class CustomerEditComponent implements OnInit {
  customerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private store: Store<fromCustomer.AppState>
  ) { }

  ngOnInit() {
    debugger
    this.customerForm = this.fb.group({
      name: ['', Validators.required],
      phone: ['', Validators.required],
      website: ['', Validators.required],
      email: ['', Validators.required],
      id: null
    });

    const customer$: Observable<Customer> = this.store.select(
      fromCustomer.getCurrentCustomer
    );

    customer$.subscribe(currentCustomer => {
      if (currentCustomer) {
        this.customerForm.patchValue({
          name: currentCustomer.name,
          phone: currentCustomer.phone,
          website: currentCustomer.website,
          email: currentCustomer.email,
          id: currentCustomer.id
        });
      }
    });
  }

  updateCustomer() {
    const updatedCustomer: Customer = {
      name: this.customerForm.get('name').value,
      phone: this.customerForm.get('phone').value,
      website: this.customerForm.get('website').value,
      email: this.customerForm.get('email').value,
      id: this.customerForm.get('id').value
    };
    this.store.dispatch(new customerActions.UpdateCustomer(updatedCustomer));
  }
}
