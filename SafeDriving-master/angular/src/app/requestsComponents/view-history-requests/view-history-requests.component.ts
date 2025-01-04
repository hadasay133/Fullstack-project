import { Component, OnInit } from '@angular/core';
import { request } from 'src/app/Module/request';
import { RequestsService } from 'src/app/Services/requests.service';

@Component({
  selector: 'app-view-history-requests',
  templateUrl: './view-history-requests.component.html',
  styleUrls: ['./view-history-requests.component.css']
})
export class ViewHistoryRequestsComponent implements OnInit {

  requests: request[] = [];
  constructor(private reqService: RequestsService) { }
  personId = 0;
  ngOnInit(): void {
    this.personId = +localStorage.getItem('id');
    this.getPersonRequests();
  }

  getPersonRequests() {
    this.reqService.getHistoryWithOffersByPersonId(this.personId).subscribe(x => {
      this.requests = x;
    });
   

  }

}
