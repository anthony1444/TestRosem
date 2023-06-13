import {Component, Input, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, UntypedFormGroup, Validators} from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MarketplaceApiService } from 'src/app/core/marketplace-api/marketplace-api.service';
import { CategoryModel } from 'src/app/core/marketplace-api/models/category.model';
import { OfferModel } from 'src/app/core/marketplace-api/models/offer.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-offer-creation',
  templateUrl: './offer-creation.component.html',
  styleUrls: ['./offer-creation.component.scss']
})
export class OfferCreationComponent implements OnInit {

  // offerForm: UntypedFormGroup;

  @Input()
  categories: string[];
  form: FormGroup;

  constructor(private formBuilder: FormBuilder, public markeplaceApiService: MarketplaceApiService, public dialogRef: MatDialogRef<OfferCreationComponent>, private snackBar: MatSnackBar) {

  }
  ngOnInit(): void {
    this.form = this.formBuilder.group({
      title: ['', Validators.required],
      picture: ['', Validators.required],
      description: ['', Validators.required],
      location: ['', Validators.required],
      categoryId: ['', Validators.required]
    });
  }

  onSubmit() {
    const offer =  new OfferModel();
    if (this.form.valid) {
      offer.category =  new CategoryModel();
      offer.categoryId = this.form.value.categoryId;
      offer.description = this.form.value.description;
      offer.location = this.form.value.location;
      offer.pictureUrl = this.form.value.picture;
      offer.title = this.form.value.title;


      this.markeplaceApiService.addOffer(offer).subscribe({next:(data=>{
        if (data===1) {
          this.showOfferCreatedAlert()
        }

      })})
    }
    this.markeplaceApiService.addOffer(offer)
  }

  closeDialog(): void {
    this.dialogRef.close();
  }

  showOfferCreatedAlert(): void {
    this.snackBar.open('The offer has been created', 'Close', {
      duration: 3000, // Duration in milliseconds
    });
  }
}
