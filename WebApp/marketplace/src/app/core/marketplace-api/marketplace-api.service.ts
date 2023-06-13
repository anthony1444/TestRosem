import {Injectable} from '@angular/core';
import {Observable, of} from 'rxjs';
import {OfferModel} from './models/offer.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class MarketplaceApiService {

  private readonly marketplaceApUrl = '';

  constructor(public http:HttpClient) { }

  getOffers(page: number, pageSize: number): Observable<OfferModel[]> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.get<OfferModel[]>('http://localhost:5000/offer/' + page.toString() + '/' + pageSize.toString() , {headers})
  }

  getOffersCount(): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.get('http://localhost:5000/offer/count' , {headers})
  }

  addOffer(offer:any): Observable<any> {
    console.log(offer);

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.post<number>('http://localhost:5000/offer', offer)
  }

  postOffer(): Observable<string> {
    // TODO: implement the logic to post a new offer, also validate whatever you consider before posting
    return of('');
  }

  getCategories(): Observable<string[]> {
    // TODO: implement the logic to retrieve the categories from the service
    return of([]);
  }
}
