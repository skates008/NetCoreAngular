import { Component, OnInit } from '@angular/core';
import {CustomersService} from "../../services/customer.service";
import {ActivatedRoute, Router} from "@angular/router";
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import {select, Store} from "@ngrx/store";
import { CustomerState } from '../../store/customer.reducer';
import {Customer, CustomerModel} from "../../models/customer";
import { selectCustomer } from '../../store/customer.selectors';
import * as CustomerActions from '../../store/customer.actions';
import {Observable} from "rxjs";
import { Update } from '@ngrx/entity';

@Component({
  selector: 'app-customer-add',
  templateUrl: './customer-add.component.html',
  styleUrls: ['./customer-add.component.css']
})
export class CustomerAddComponent implements OnInit {
  customer$: Observable<Customer>;
  customer: Customer;
  customerForm: FormGroup;
  isEditMode: boolean = false;
  constructor(private store: Store<CustomerState>,
              private router: Router,
              private route: ActivatedRoute,
              private formBuilder: FormBuilder,
              private customersService: CustomersService) { }

  ngOnInit(): void {
    let id = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!(id);
    if(id) {
      this.store.dispatch(
        CustomerActions.loadCustomer({id: id})
      )
      this.customer$  = this.store.pipe(select(selectCustomer));
      this.customer$.subscribe((emp) => {
          this.customer = emp;
          this.loadFormControl(emp);
      })
    } else {
      this.loadFormControl(null);
    }

 
  }
  loadFormControl(customer) {
    this.customerForm = this.formBuilder.group({
      name: [customer ? customer.name : null, [Validators.required] ],
      email: [customer ? customer.email : null, [Validators.required] ],
      phone: [customer ? customer.phone : null, [Validators.required] ],
      address: [customer ? customer.address : null, [Validators.required] ],
    })
  }
  onSubmit() {
      if(this.isEditMode) {
        const update: Update<Customer> = {
          id: this.customer.id,
          changes: this.customerForm.value
        }
        this.store.dispatch(CustomerActions.updateCustomer({customer: update }))
      } else {
          console.log("Add new ", this.customerForm.value)
          this.store.dispatch(CustomerActions.addCustomer({ customer: this.customerForm.value}))
      }
  }

}
