import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AppComponent} from "./app.component";
import {HomeComponent} from "./pages/home/home.component";


const routes: Routes = [
  {path: "", component: HomeComponent},
  {
    path: "customer",
    loadChildren:() => import('./customer/customer.module').then(m => m.CustomersModule)
  },
   
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
