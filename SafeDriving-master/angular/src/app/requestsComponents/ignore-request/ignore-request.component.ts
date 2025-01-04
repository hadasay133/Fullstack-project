import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { RequestsService } from '../../Services/requests.service';
import { OffersService } from 'src/app/Services/offers.service';
@Component({
  selector: 'app-ignore-request',
  templateUrl: './ignore-request.component.html',
  styleUrls: ['./ignore-request.component.css']
})
export class IgnoreRequestComponent implements OnInit {

  isLoading = true;
  message = '';
  constructor(private route: ActivatedRoute, private requests: RequestsService, private offerSer: OffersService) { }
  offerId = 0;
  reqId = 0;
  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {

      this.offerId = this.route.snapshot.params['offerId'];
      this.reqId = this.route.snapshot.params['reqId'];
      this.offerSer.ignoreOfferWithRequests(this.offerId, this.reqId).subscribe(x => {

        if (x === 1) {
          this.isLoading = false;
          this.message = "ההצעה נדחתה בהצלחה!";
        }
        else if (x === 2) {
          this.isLoading = false;
          this.message = "הרשמתך בעבר התבטלה, ההצעה נדחתה";

        }
        else if (x === 3) {
          this.isLoading = false;
          this.message = "דחייתך כבר נקלטה בעבר!";


        }
        else if (x === 4) {
          this.isLoading = false;
          this.message = "הנסיעה לא קיימת במערכת";

        }
        else {
          this.isLoading = false;
          this.message = "הבקשה לא קיימת במערכת";

        }
      });
    });
  }


}
