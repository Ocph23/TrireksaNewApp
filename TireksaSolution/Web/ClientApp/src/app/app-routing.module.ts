import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {TracingComponent} from './tracing/tracing.component';
import {PriceComponent} from './price/price.component';
import {OurServiceComponent} from './our-service/our-service.component';
import {HomeComponent} from './home/home.component';
  import { from } from 'rxjs';
const routes: Routes = [
  {path:'',component:HomeComponent},
  {path:'services',component:OurServiceComponent},
  {path:'price',component:PriceComponent},
  {path:'tracing',component:TracingComponent}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
