import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {CustomerListComponent} from "./components/customer-list/customer-list.component";
import {CustomerAddComponent} from "./components/customer-add/customer-add.component";


const routes: Routes = [
  { path: "", component: CustomerListComponent },
  { path: "add", component: CustomerAddComponent },
  { path: "edit/:id", component: CustomerAddComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomersRoutingModule { }
