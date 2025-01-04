import { Component, OnInit } from '@angular/core';
import { Offer } from 'src/app/Module/offer';
import { OffersService } from 'src/app/Services/offers.service';

@Component({
  selector: 'app-view-history-offers',
  templateUrl: './view-history-offers.component.html',
  styleUrls: ['./view-history-offers.component.css']
})
export class ViewHistoryOffersComponent implements OnInit {
  offers: Offer[] = [];
  constructor(private offerService: OffersService) { }
  personId = 0;
  ngOnInit(): void {
    this.personId = +localStorage.getItem('id');
    this.getPersonOffers();
  }

  getPersonOffers() {
    this.offerService.getHistoryByPersonId(this.personId).subscribe(x => {
      this.offers = x;
    });
   

  }

}
