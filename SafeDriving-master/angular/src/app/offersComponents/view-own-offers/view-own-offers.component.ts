import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Offer } from 'src/app/Module/offer';
import { OffersService } from 'src/app/Services/offers.service';
import { RequestsService } from 'src/app/Services/requests.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-view-own-offers',
  templateUrl: './view-own-offers.component.html',
  styleUrls: ['./view-own-offers.component.css']
})
export class ViewOwnOffersComponent implements OnInit {

  offers: Offer[] = [];
  offerssWithTravels: Offer[] = [];
  constructor(private offService: OffersService,
    private router: Router, private offerService: OffersService) { }
  personId = 0;
  ngOnInit(): void {
    this.personId = +localStorage.getItem('id');
    this.getPersonOffers();
  }

  getPersonOffers() {
    this.offerService.getWithNoRequestsByPersonId(this.personId).subscribe(x => {
      this.offers = x;
    });
    this.offerService.getWithRequestsByPersonId(this.personId).subscribe(x => {
      this.offerssWithTravels = x;
    });


  }
  editOff(id: number) {
    this.router.navigateByUrl('privateArea/updateOffer/' + id)
  }



  addHours(date, h) {
    date.setTime(date.getTime() + (h * 60 * 60 * 1000));
    return new Date(date);
  }
  //Alert
  removeOffTravel(off: Offer) {


    Swal.fire({
      title: 'האם את/ה בטוח/ה?',
      text: "מחיקת הנסיעה תשלח מייל לנוסעים על הביטול ועלולה לפגוע בשם שלך, מחיקות מרובות ישהו את המשתמש שלך מהמערכת",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',

      confirmButtonText: 'כן, למרות זאת',
      cancelButtonText: 'לא, לא למחוק',
    }).then((result) => {
      if (result.isConfirmed) {
        this.offerService.removeOfferWithTravels(off.id).subscribe((x) => {
          this.getPersonOffers();
        });
      }
    });


  }


  deleteOff(off: Offer) {

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
        this.offerService.delete(off.id).subscribe((x) => {
          this.offerService.getWithNoRequestsByPersonId(this.personId).subscribe(x => {
            this.offers = x;
          });
        });
      }
    });

  }


}
