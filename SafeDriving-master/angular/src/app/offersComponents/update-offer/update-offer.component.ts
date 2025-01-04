import { Component, OnInit } from '@angular/core';
import { Offer } from '../../Module/offer';
import { OffersService } from '../../Services/offers.service';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { request } from '../../Module/request';
import { ActivatedRoute, Router } from '@angular/router';
import { TravelsService } from 'src/app/Services/travels.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-update-offer',
  templateUrl: './update-offer.component.html',
  styleUrls: ['./update-offer.component.css']
})
export class UpdateOfferComponent implements OnInit {
  id: number = 1234;//לקבל את זה מהקומפוננט האב
  offer: Offer = null;
  public lCity = [
    "ירושלים", "תל אביב-יפו", "חיפה", "ראשון לציון", "פתח תקווה", "נתניה", "אשדוד", "בני ברק", "באר שבע", "חולון", "רמת גן", "אשקלון", "רחובות", "בית שמש", "בת ים", "הרצליה", "כפר סבא", "חדרה", "מודיעין- מכבים- רעות", "לוד", "מודיעין עילית", "רעננה", "נצרת", "רמלה", "רהט", "ראש העין", "הוד השרון", "ביתר עילית", "גבעתיים", "נהריה", "קריית גת", "קריית אתא", "עפולה", "אום אל-פחם", "יבנה", "אילת", "נס ציונה", "אלעד", "עכו", "רמת השרון", "טבריה", "קריית מוצקין", "כרמיאל", "טייבה", "קריית ביאליק", "נוף הגליל", "שפרעם", "נתיבות", "קריית אונו", "קריית ים", "מעלה אדומים", "צפת", "אור יהודה", "דימונה", "טמרה", "אופקים", "סחנין", "שדרות", "באקה אל-גרבייה", "יהוד-מונוסון", "באר יעקב", "גבעת שמואל", "כפר יונה", "ערד", "טירה", "טירת כרמל", "עראבה", "מגדל העמק", "קריית מלאכי", "כפר קאסם", "יקנעם עילית", "קלנסווה", "נשר", "קריית שמונה", "מעלות-תרשיחא", "אור עקיבא", "אריאל", "בית שאן"
  ];
  requestList: request[];
  myForm = new FormGroup({
    resourse: new FormControl(null, [Validators.required]),
    destination: new FormControl(null, [Validators.required]),
    seats: new FormControl('1', [Validators.required, Validators.min(1)]),
    date_time: new FormControl(null, [Validators.required]),
    resourse_city: new FormControl(null, [Validators.required]),
    destination_city: new FormControl(null, [Validators.required]),
    id: new FormControl('0'),
    active: new FormControl('1'),
    ignore_offers: new FormControl(null),
    remarks: new FormControl(null, [Validators.required]),
    price: new FormControl('0', [Validators.required, Validators.min(0)]),
  });
  constructor(private router: Router, private offers: OffersService, private route: ActivatedRoute,
    private fb: FormBuilder, public travelSer: TravelsService) { }

  ngOnInit(): void {

    this.route.queryParams.subscribe(params => {

      var id = this.route.snapshot.params['id'];
      this.offers.getById(id).subscribe(x => {
        this.myForm = this.fb.group(x);
        this.myForm.patchValue({ date_time: new Date(new Date(x.date_time).setTime(new Date(x.date_time).getTime() + (3 * 60 * 60 * 1000))).toISOString().substring(0, 16) })
      });
    });
  }

  back(){

    this.router.navigateByUrl('privateArea/OwnOffers');


  }
  reset() {
    this.myForm.reset('',);
  }
  onSubmit(): void {

    var r = <Offer>this.myForm.value;

    this.offers.update(r).subscribe(res => {
      if (res == null) {

        Swal.fire('', "ארעה שגיאה במערכת", 'error');
      }
      else {
        Swal.fire('', "הצעת הנסיעה נוספה בהצלחה", 'success');
        this.router.navigateByUrl('privateArea/OwnOffers');

      }
      // this.offerList = res;
      //debugger;
    }, err => { console.log("not good " + JSON.stringify(err)) });


  
  }
}
