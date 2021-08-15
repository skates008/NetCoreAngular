import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { map, mergeMap, concatMap, catchError, tap } from 'rxjs/operators';
import { CustomersService } from '../services/customer.service';
import * as fromCustomerActions from './customer.actions';
import { Customer } from '../models/customer';
import { Router } from '@angular/router';

@Injectable()
export class CustomerEffects {

  loadCustomers$ = createEffect(() => this.actions$.pipe(
    ofType(fromCustomerActions.loadCustomers),
    mergeMap(() => this.customersService.getCustomers()
      .pipe(
        map(customers => fromCustomerActions.loadCustomersSuccess({ customers})),
        catchError((error) =>
          of(fromCustomerActions.loadCustomersFailure({ error })
        )
      )
    )
  )));

  loadCustomer$ = createEffect(() => this.actions$.pipe(
    ofType(fromCustomerActions.loadCustomer),
    mergeMap((action) => this.customersService.getCustomer(action.id)
      .pipe(
        map(customer => fromCustomerActions.loadCustomerSuccess({ selectedCustomer: customer})),
        catchError((error) =>
          of(fromCustomerActions.loadCustomersFailure({ error })
        )
      )
    )
  )));

  createCustomer$ = createEffect(() => this.actions$.pipe(
    ofType(fromCustomerActions.addCustomer),
    mergeMap((action) => this.customersService.createCustomer(action.customer)
      .pipe(
        map(customer => fromCustomerActions.addCustomerSuccess({ customer})),
        catchError((error) =>
          of(fromCustomerActions.addCustomerFailure({ error }))
        )
      )
    ),
    tap(() => this.router.navigate(['/customer']))
  ));

  deleteCustomer$ = createEffect(() => this.actions$.pipe(
    ofType(fromCustomerActions.deleteCustomer),
    mergeMap((action) => this.customersService.deleteCustomer(action.id)
      .pipe(
        map(customer => fromCustomerActions.deleteCustomerSuccess({ id: action.id})),
        catchError((error) =>
          of(fromCustomerActions.deleteCustomerFailure({ error }))
        )
      )
    )
  ));

  updateCustomer$ = createEffect(() => this.actions$.pipe(
    ofType(fromCustomerActions.updateCustomer),
    concatMap(action =>
      this.customersService.updateCustomer(
        action.customer.id,
        action.customer.changes
      )
    ),
    tap(() => this.router.navigate(['/customer']))
  ),
    { dispatch: false }
  );


  constructor(private actions$: Actions,
    private router: Router,
    private customersService: CustomersService) {}

}
