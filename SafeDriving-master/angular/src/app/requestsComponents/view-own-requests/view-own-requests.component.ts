import { Component, OnInit } from '@angular/core';
import { request } from 'src/app/Module/request';
import { RequestsService } from 'src/app/Services/requests.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { OffersService } from 'src/app/Services/offers.service';

@Component({
  selector: 'app-view-own-requests',
  templateUrl: './view-own-requests.component.html',
  styleUrls: ['./view-own-requests.component.css']
})
export class ViewOwnRequestsComponent implements OnInit {
  requests: request[] = [];
  requestsWithTravels: request[] = [];
  constructor(private reqService: RequestsService,
    private router: Router, private offerService: OffersService) { }
  personId = 0;
  ngOnInit(): void {
    this.personId = +localStorage.getItem('id');
    this.getPersonRequests();
  }

  getPersonRequests() {
    this.reqService.getByPersonId(this.personId).subscribe(x => {
      this.requests = x;
    });
    this.reqService.getWithOffersByPersonId(this.personId).subscribe(x => {
      this.requestsWithTravels = x;
    });


  }
  editReq(id: number) {
    this.router.navigateByUrl('privateArea/updateRequest/' + id)
  }



  addHours(date, h) {
    date.setTime(date.getTime() + (h * 60 * 60 * 1000));
    return new Date(date);
  }
  //Alert
  removeReqTravel(req: request) {

    if (new Date(req.date_time) <= new Date(this.addHours(new Date(), 24))) {

      Swal.fire('', 'לא ניתן למחוק נסיעה פחות מ24 שעות משעת היציאה, פנה לנהג', 'error');
    }
    else {
      Swal.fire({
        title: 'האם את/ה בטוח/ה?',
        text: " האם את/ה בטוח/ה שאת/ה רוצה להסיר את הנסיעה הנוכחית מהנסיעה?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',

        confirmButtonText: 'כן',
        cancelButtonText: 'לא',
      }).then((result) => {
        if (result.isConfirmed) {
          this.offerService.ignoreOfferWithRequests
            (req.offer.id, req.id).subscribe((x) => {

              if (x === 1) {
                Swal.fire('', "ההצעה נדחתה בהצלחה!", 'success');

              }
              else if (x === 2) {
                Swal.fire('', "הרשמתך בעבר התבטלה, ההצעה נדחתה", 'success');


              }
              else {
                Swal.fire('', "דחייתך כבר נקלטה בעבר!", 'warning');


              }

              this.getPersonRequests();
            });
        }
      });

    }
  }


  deleteReq(req: request) {
    if (new Date(req.date_time) <= new Date(this.addHours(new Date(), 24))) {

      Swal.fire('', 'לא ניתן למחוק נסיעה פחות מ24 שעות משעת היציאה, פנה לנהג', 'error');
    }
    else {
      Swal.fire({
        title: 'האם את/ה בטוח/ה?',
        text: "האם את/ה בטוח/ה שאת/ה רוצה למחוק את הנסיעה הנוכחית?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',

        confirmButtonText: 'כן',
        cancelButtonText: 'לא',
      }).then((result) => {
        if (result.isConfirmed) {
          this.reqService.delete(req.id).subscribe((x) => {
            this.reqService.getByPersonId(this.personId).subscribe(x => {
              this.requests = x;
            });
          });
        }
      });

    }

  }
}
