import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { request } from '../../Module/request';
import { Router, ActivatedRoute } from '@angular/router';
import { Offer } from '../../Module/offer';
import { OffersService } from '../../Services/offers.service';
import { identifierModuleUrl } from '@angular/compiler';
import Swal from 'sweetalert2';
import { TravelsService } from 'src/app/Services/travels.service';

@Component({
  selector: 'app-offers-form',
  templateUrl: './offers-form.component.html',
  styleUrls: ['./offers-form.component.css']
})
export class OffersFormComponent implements OnInit {
  public lCity = [
    "ירושלים", "תל אביב-יפו", "חיפה", "ראשון לציון", "פתח תקווה", "נתניה", "אשדוד", "בני ברק", "באר שבע", "חולון", "רמת גן", "אשקלון", "רחובות", "בית שמש", "בת ים", "הרצליה", "כפר סבא", "חדרה", "מודיעין- מכבים- רעות", "לוד", "מודיעין עילית", "רעננה", "נצרת", "רמלה", "רהט", "ראש העין", "הוד השרון", "ביתר עילית", "גבעתיים", "נהריה", "קריית גת", "קריית אתא", "עפולה", "אום אל-פחם", "יבנה", "אילת", "נס ציונה", "אלעד", "עכו", "רמת השרון", "טבריה", "קריית מוצקין", "כרמיאל", "טייבה", "קריית ביאליק", "נוף הגליל", "שפרעם", "נתיבות", "קריית אונו", "קריית ים", "מעלה אדומים", "צפת", "אור יהודה", "דימונה", "טמרה", "אופקים", "סחנין", "שדרות", "באקה אל-גרבייה", "יהוד-מונוסון", "באר יעקב", "גבעת שמואל", "כפר יונה", "ערד", "טירה", "טירת כרמל", "עראבה", "מגדל העמק", "קריית מלאכי", "כפר קאסם", "יקנעם עילית", "קלנסווה", "נשר", "קריית שמונה", "מעלות-תרשיחא", "אור עקיבא", "אריאל", "בית שאן"
  ];
  today = new Date();
  offer = new Offer();
  id: number;
  myForm = new FormGroup({
    resourse: new FormControl(null, [Validators.required]),
    destination: new FormControl(null, [Validators.required]),
    seats: new FormControl('1', [Validators.required, Validators.min(1)]),
    date_time: new FormControl(null, [Validators.required]),
    resourse_city: new FormControl(null, [Validators.required]),
    destination_city: new FormControl(null, [Validators.required]),
    id: new FormControl('0'),
    active: new FormControl('1'),
    ignore_offers: new FormControl(''),
    remarks: new FormControl(null, [Validators.required]),
    price: new FormControl('0', [Validators.required, Validators.min(0)]),
  });

  constructor(private router: Router, private offers: OffersService, private route: ActivatedRoute,
    private fb: FormBuilder, public travelSer: TravelsService) { }

  ngOnInit(): void {
   this.today = new Date();
  }

  reset() {
    this.myForm.reset('',);
  }
  onSubmit(): void {

    var r = <Offer>this.myForm.value;
    r.active = true;
    var id = +localStorage.getItem('id');

    r.id_person = id;
    this.offers.addAndGet(r).subscribe(res => {
      if (res == null) {

        Swal.fire('', "ארעה שגיאה במערכת", 'error');
      }
      else {
        Swal.fire('', "הצעת הנסיעה נוספה בהצלחה", 'success');
        this.router.navigateByUrl('privateArea/ActiveRequests/' + res);

      }
      // this.offerList = res;
      //debugger;
    }, err => { console.log("not good " + JSON.stringify(err)) });



    // //debugger;
    // this.offer.id_person = +localStorage.getItem('id');
    // this.offer.date_time = this.myForm.value.dateTime;
    // this.offer.price = this.myForm.value.price;
    // this.offer.seats = this.myForm.value.seats;
    // this.offer.destination = this.myForm.value.destination;
    // this.offer.resourse = this.myForm.value.resource;
    // this.offer.id = 0;
    // this.offer.active = true;



    // this.offers.addAndGet(this.offer).subscribe(res => {
    //   console.log("הנסיעה נוספה");
    //   if (res == null) {

    //     alert("No suitable trips were found")
    //   }
    //   else {
    //     this.requestList = res;
    //     //הכנסת הנתונים למערך
    //     //בקשת נסיעות מתאימות, קבלתם והצגתם למשתמש לצורך בחירה
    //     // debugger;

    //   }

    // }
    //   , err => { console.log("not good :(") });


  }
  onClick(id: number): void {
    this.offers.selectOfferByRequestId(id, this.offer.id).subscribe(res => {

      console.log("Please wait! ")

    }, err => { console.log("Operation failed ") });



  }

}





