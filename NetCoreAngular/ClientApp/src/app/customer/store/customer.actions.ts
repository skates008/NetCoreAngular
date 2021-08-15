import { createAction, props } from '@ngrx/store';
import { Update } from '@ngrx/entity';

import { Customer } from '../models/customer';

export const loadCustomers = createAction(
  '[Customer/API] Load Customers'
);
export const loadCustomersSuccess = createAction(
  '[Customer/API] Load Customers success',
  props<{ customers: Customer[] }>()
);
export const loadCustomersFailure = createAction(
  '[Customer/API] Load Customers Failure',
  props<{ error: any }>()
);

export const loadCustomer = createAction(
  '[Customer/API] Load Customer',
  props<{ id: string }>()
);
export const loadCustomerSuccess = createAction(
  '[Customer/API] Load Customer success',
  props<{ selectedCustomer: Customer }>()
);
export const loadCustomerFailure = createAction(
  '[Customer/API] Load Customer Failure',
  props<{ error: any }>()
);


// Add customer
export const addCustomer = createAction(
  '[Customer/API] Add Customer',
  props<{ customer: Customer }>()
);
export const addCustomerSuccess = createAction(
  '[Customer/API] Add Customer Success',
  props<{ customer: Customer }>()
);
export const addCustomerFailure = createAction(
  '[Customer/API] Add Customer Failure',
  props<{ error: any }>()
);

// Update customer
export const updateCustomer = createAction(
  '[Customer/API] Update Customer',
  props<{ customer: Update<Customer> }>()
);


// Delete customer
export const deleteCustomer = createAction(
  '[Customer/API] Delete Customer',
  props<{ id: string }>()
);
export const deleteCustomerSuccess = createAction(
  '[Customer/API] delete Customer Success',
  props<{ id: string }>()
);
export const deleteCustomerFailure = createAction(
  '[Customer/API] delete Customer Failure',
  props<{ error: any }>()
);


