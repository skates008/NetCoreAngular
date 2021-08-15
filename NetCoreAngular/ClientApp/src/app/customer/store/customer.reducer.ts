import { Action, createReducer, on } from '@ngrx/store';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';
import { Customer } from '../models/customer';
import * as CustomerActions from './customer.actions';

export const customersFeatureKey = 'customers';

export interface CustomerState extends EntityState<Customer> {
  // additional entities state properties
  error: any;
  selectedCustomer: Customer
}

export const adapter: EntityAdapter<Customer> = createEntityAdapter<Customer>();

export const initialState: CustomerState = adapter.getInitialState({
  // additional entity state properties
  error: undefined,
  selectedCustomer: undefined
});


export const customerReducer = createReducer(
  initialState,

  // loadCustomers
  on(CustomerActions.loadCustomersSuccess,
    (state, action) => adapter.setAll(action.customers, state)
  ),
  on(CustomerActions.loadCustomersFailure, (state, action) => {
      return {
        ...state,
        error: action.error
      }
    }
  ),

// loadCustomer
  on(CustomerActions.loadCustomerSuccess, (state, action) => {
      return {
        ...state,
        selectedCustomer: action.selectedCustomer
      }
    }
  ),
  on(CustomerActions.loadCustomerFailure, (state, action) => {
      return {
        ...state,
        error: action.error
      }
    }
  ),

  // Add Customer
  on(CustomerActions.addCustomerSuccess, (state, action) =>
    adapter.addOne(action.customer, state)
  ),
  on(CustomerActions.addCustomerFailure, (state, action) => {
    return {
      ...state,
      error: action.error
    }
  }),

  // Update Customer
  on(CustomerActions.updateCustomer, (state, action) =>
    adapter.updateOne(action.customer, state)
  ),

  on(CustomerActions.deleteCustomerSuccess, (state, action) =>
    adapter.removeOne(action.id, state)
  ),

);

export function reducer(state: CustomerState | undefined, action: Action) {
  return customerReducer(state, action);
}

export const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal,
} = adapter.getSelectors();
