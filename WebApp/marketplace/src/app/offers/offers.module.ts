import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {OfferItemComponent} from './offer-item/offer-item.component';
import {OfferCreationComponent} from './offer-creation/offer-creation.component';
import {OfferListComponent} from './offer-list/offer-list.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { OfferComponent } from './Components/offer/offer.component';
import { EnumeratePipe } from './pipes/enumerate/enumerate.pipe';
import {MatDialogModule} from '@angular/material/dialog';
import {MatButtonModule} from '@angular/material/button';
import { MatSnackBarModule } from '@angular/material/snack-bar';



@NgModule({
  declarations: [OfferItemComponent, OfferCreationComponent, OfferListComponent, OfferComponent, EnumeratePipe],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatDialogModule,
    MatButtonModule,
    MatSnackBarModule
  ], exports:[OfferComponent]
})
export class OffersModule { }
