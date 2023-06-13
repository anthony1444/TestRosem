import { Component, Input } from '@angular/core';
import { OfferModel } from 'src/app/core/marketplace-api/models/offer.model';

@Component({
  selector: 'app-offer',
  templateUrl: './offer.component.html',
  styleUrls: ['./offer.component.scss']
})
export class OfferComponent {
@Input('offer') offer:OfferModel

}
