import { createFeatureSelector, createSelector } from '@ngrx/store';
import { customersFeatureKey, CustomerState, selectAll } from './customer.reducer';

export const selectCustomerState = createFeatureSelector<CustomerState>(
  customersFeatureKey
);

export const selectCustomers = createSelector(selectCustomerState, selectAll);
export const selectCustomer = createSelector(
  selectCustomerState,
  (state: CustomerState) => state.selectedCustomer
);
