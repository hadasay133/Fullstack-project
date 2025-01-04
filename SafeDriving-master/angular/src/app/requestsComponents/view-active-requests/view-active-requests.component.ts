import { Component, OnInit } from '@angular/core';
import { RequestsService } from 'src/app/Services/requests.service';
import { Router, ActivatedRoute } from '@angular/router';
import { request } from 'src/app/Module/request';
import { OffersService } from 'src/app/Services/offers.service';
import { Offer } from 'src/app/Module/offer';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-view-active-requests',
  templateUrl: './view-active-requests.component.html',
  styleUrls: ['./view-active-requests.component.css']
})
export class ViewActiveRequestsComponent implements OnInit {

  constructor(private requestService: RequestsService,
    private offerService: OffersService, private router: Router, private route: ActivatedRoute) { }
  offers: Offer[] = [];
  requests: request[] = [];
  activeAequests: request[] = [];
  disabledAequests: request[] = [];
  offerId = 0;
  offerSeats = 0;
  offerTravelsSeats = 999;

  selectOffer(off) {
    this.offerId = off;
    this.offerSeats = this.offers.filter(x => x.id === +off)[0].seats;

    this.requests = [];
    this.activeAequests = [];
    this.disabledAequests = [];
    this.getRequests();

  }
  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      var pId = +localStorage.getItem('id');
      this.offerId = +this.route.snapshot.params['id'];

      if (this.offerId && this.offerId !== 0) {
        this.getRequests();
      }


      this.offerService.getByPersonId(pId).subscribe(x => {
        this.offers = x;
        if ((!this.offerId || this.offerId === 0) && this.offers.length > 0) {
          this.offerId = this.offers[0].id;

          this.getRequests();
        }
        if (this.offers.length > 0) {
          this.offerSeats = this.offers[0].seats;
        }

      });

    });
  }
  sendEmailWithOffer(rId) {
    this.requestService.sendEmailWithOffer(rId, this.offerId).subscribe(x => {
      if (x) {
        Swal.fire('', 'ההודעה נשלחה בהצלחה', 'success');
        this.requestService.getActiveRelevantByOfferId(this.offerId).subscribe(x => {
          this.activeAequests = x;
        });
      }
    });
  }
  getRequests() {
    this.offerService.getNumSeatsByOfferId(this.offerId).subscribe(x => {
      this.offerTravelsSeats = x;
    });
    this.requestService.getActiveRelevantByOfferId(this.offerId).subscribe(x => {
      this.activeAequests = x;
    });
    this.requestService.getRelevantWithOffersByOfferId(this.offerId).subscribe(x => {
      this.requests = x;
    });
    this.requestService.getDisabledWithOffersByOfferId(this.offerId).subscribe(x => {
      this.disabledAequests = x;
    });
  }
}
