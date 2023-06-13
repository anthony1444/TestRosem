import { Component, OnInit } from "@angular/core";
import { MarketplaceApiService } from "src/app/core/marketplace-api/marketplace-api.service";
import { OfferModel } from "src/app/core/marketplace-api/models/offer.model";
import {MatDialog } from '@angular/material/dialog';
import { OfferCreationComponent } from "../offer-creation/offer-creation.component";

@Component({
  selector: "app-offer-list",
  templateUrl: "./offer-list.component.html",
  styleUrls: ["./offer-list.component.scss"],
})
export class OfferListComponent implements OnInit {
  pageSize = 10;
  page = 1;
  offers: OfferModel[] = [];
  total = 0;
  totalPages = 0;
  numbers: number[];
  pageIndex = 0;
  PageCount = 10


  constructor(public markeplaceApiService: MarketplaceApiService, public dialog:MatDialog) {}

  ngOnInit(): void {
    this.markeplaceApiService.getOffersCount().subscribe((data) => {
      this.total = data;
      this.totalPages = Math.round(this.total / this.pageSize);
      this.pageIndex = this.page
      this.numbers =  Array.from({length:this.PageCount},(v,k)=>k+1);
    });

    this.markeplaceApiService.getOffers(this.page, this.pageSize).subscribe({
      next: (offers: OfferModel[]) => {
        this.offers = offers;
      },
    });
  }

  getOffersPage(page:number,pageSize:number) {
    this.pageIndex =  page;
    this.markeplaceApiService.getOffers(page, pageSize).subscribe({
      next: (offers: OfferModel[]) => {
        this.offers = offers;
      },
    });
  }

  GetNextPageIndexes(){
    if (this.pageIndex=== this.totalPages) {
      return;
    }
    this.pageIndex++;
    if (this.numbers[this.totalPages - 1] < this.totalPages) {
      this.numbers = this.numbers.map(n => n + 1);
    }

    this.getOffersPage(this.pageIndex,this.pageSize);
  }

  GetPreviousPageIndexes(){
    if (this.pageIndex=== 1) {
      return;
    }
    this.pageIndex--;
    if (this.numbers[0] > 1) {
      this.numbers = this.numbers.map(n => n - 1);
    }

    this.getOffersPage(this.pageIndex,this.pageSize);
  }


  getFirstOffersPage(){

    this.numbers =  Array.from({length:this.PageCount},(v,k)=>k+1);
    this.pageIndex = 1;
    this.getOffersPage(this.pageIndex,this.pageSize);

  }

  getLastOffersPage(){
    const lastNumbers = this.totalPages - this.PageCount;
    this.getFirstOffersPage();
    this.numbers = this.numbers.map(n => n + lastNumbers);
    this.pageIndex = this.totalPages;
    this.getOffersPage(this.pageIndex,this.pageSize);
  }

  openDialog(){
    this.dialog.open(OfferCreationComponent, {
      width: '400px',
      height:'600px'
    });
  }
}
