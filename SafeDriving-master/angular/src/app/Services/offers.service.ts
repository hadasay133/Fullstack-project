import { Injectable } from '@angular/core';
import { Offer } from '../Module/offer'
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { request } from '../Module/request';
//import { person } from '../Module/person';
//import { Observable, from } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class OffersService {
  url = "http://localhost:50940/api/offers/"

  constructor(private http: HttpClient) { }
  addAndGet(offer: Offer): Observable<number> {
    //debugger;
    return this.http.post<number>(this.url + "AddOffer", offer);
  }
  update(offer: Offer): Observable<number> {
    //debugger;
    return this.http.post<number>(this.url + "updateOffer", offer);
  }

  removeOfferWithTravels(id: number): Observable<boolean> {
    //debugger;
    return this.http.delete<boolean>(this.url + "removeOfferWithTravels?id=" + id);
  }
  getAllActiveOffersByReqId(id: number): Observable<Offer[]> {
    //debugger;
    return this.http.get<Offer[]>(this.url + "getAllActiveOffersByReqId?id=" + id);
  }

  getNumSeatsByOfferId(offerId: number): Observable<number> {//יש הבדל אם חוזר list או arr?

    return this.http.get<number>(this.url + "getNumSeatsByOfferId" + "?id=" + offerId);

  }

  checkSetOfferWithRequests(id: number, reqId: number): Observable<number> {
    //debugger;
    return this.http.get<number>(this.url + "checkSetOfferWithRequests" + "?id=" + id + "&reqId=" + reqId);
  }
  ignoreOfferWithRequests(id: number, reqId: number): Observable<number> {
    //debugger;
    return this.http.get<number>(this.url + "ignoreOfferWithRequests" + "?id=" + id + "&reqId=" + reqId);
  }


  getAll(): Offer[] {
    return //?F
  }
  getById(id: number) {
    //debugger;
    return this.http.get<Offer>(this.url + "getById/" + "?id=" + id);

  }


  getHistoryByPersonId(id: number): Observable<Offer[]> {//יש הבדל אם חוזר list או arr?
    //debugger;
    return this.http.get<Offer[]>(this.url + "getHistoryByPersonId" + "?id=" + id);
  }

  getAllActiveOffers(): Observable<Offer[]> {//יש הבדל אם חוזר list או arr?
    //debugger;
    return this.http.get<Offer[]>(this.url + "getAllActiveOffers");
  }
  
  getByPersonId(id: number): Observable<Offer[]> {//יש הבדל אם חוזר list או arr?
    //debugger;
    return this.http.get<Offer[]>(this.url + "getByPersonId" + "?id=" + id);
  }

  getWithRequestsByPersonId(id: number): Observable<Offer[]> {//יש הבדל אם חוזר list או arr?
    //debugger;
    return this.http.get<Offer[]>(this.url + "GetWithRequestsByPersonId" + "?id=" + id);
  }
  getWithNoRequestsByPersonId(id: number): Observable<Offer[]> {//יש הבדל אם חוזר list או arr?
    //debugger;
    return this.http.get<Offer[]>(this.url + "getWithNoRequestsByPersonId" + "?id=" + id);
  }

  delete(id: number): Observable<boolean> {
    return this.http.post<boolean>(this.url, "?id=" + id);
  }
  selectOfferByRequestId(offerId: number, reqId: number): Observable<boolean> {
    return this.http.get<boolean>(this.url
      + 'selectOfferByRequestId/' + offerId + '/' + reqId);
  }





}




