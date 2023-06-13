import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from './authentication/login/login.component';
import {OfferListComponent} from './offers/offer-list/offer-list.component';
import {OfferCreationComponent} from './offers/offer-creation/offer-creation.component';


const routes: Routes = [
  {path: 'a', component: LoginComponent},
  {path: 'b', component: OfferListComponent},
  {path: 'c', component: OfferCreationComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
