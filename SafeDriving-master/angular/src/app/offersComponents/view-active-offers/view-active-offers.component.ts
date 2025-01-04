import { Component, OnInit } from '@angular/core';
import { Offer } from 'src/app/Module/offer';
import { request } from 'src/app/Module/request';
import { OffersService } from 'src/app/Services/offers.service';
import { RequestsService } from 'src/app/Services/requests.service';
import { TravelsService } from 'src/app/Services/travels.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-view-active-offers',
  templateUrl: './view-active-offers.component.html',
  styleUrls: ['./view-active-offers.component.css']
})
export class ViewActiveOffersComponent implements OnInit {
  offers: Offer[] = [];
  requests: request[] = [];
  reqId = 0;

  personId: number;
  constructor(private offerService: OffersService, public recSer: RequestsService) { }

  ngOnInit(): void {
    this.personId = +localStorage.getItem('id');
    this.getPersonActiveRequests();
   
  }
  getPersonActiveRequests() {
    this.recSer.getByPersonId(this.personId).subscribe(x => {
      this.requests = x;
      if(this.requests.length > 0)
      {
        this.reqId = this.requests[0].id;
        this.getPersonOffers();

      }
    
    });
  }
  getPersonOffers() {
    this.offerService.getAllActiveOffersByReqId(this.reqId).subscribe(x => {
      this.offers = x;
    });
  }

  selectRequest(id) {
    this.reqId = id;
    this.requests = [];
    this.getPersonActiveRequests();


  }
  sendEmailWithOffer(offId) {
    this.recSer.sendEmailToOffer(this.reqId,offId).subscribe(x => {
      if (x) {
        Swal.fire('', 'ההודעה נשלחה בהצלחה', 'success');
        this.getPersonOffers();
      }
    });
  }
}
