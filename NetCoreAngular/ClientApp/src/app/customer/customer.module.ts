import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomersRoutingModule } from './customer-routing.module';
import { StoreModule } from '@ngrx/store';
import { CustomerListComponent } from './components/customer-list/customer-list.component';
import { EffectsModule } from '@ngrx/effects';
import { CustomerEffects } from './store/customer.effects';
import {CustomersService} from "./services/customer.service";
import { CustomerAddComponent } from './components/customer-add/customer-add.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import * as fromCustomer from './store/customer.reducer';


@NgModule({
  declarations: [CustomerListComponent, CustomerAddComponent],
  imports: [
    CommonModule,
    CustomersRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    StoreModule.forFeature(
      fromCustomer.customersFeatureKey,
      fromCustomer.reducer),
    EffectsModule.forFeature([CustomerEffects]),
    StoreModule.forFeature(fromCustomer.customersFeatureKey, fromCustomer.reducer)
  ],
  providers:[CustomersService ],
  exports: [
   CustomerListComponent
  ]
})
export class CustomersModule { }
