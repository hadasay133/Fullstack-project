import { Component, OnInit } from '@angular/core';
import { RequestsService } from '../../Services/requests.service';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { request } from '../../Module/request';
import { Offer } from '../../Module/offer';
import Swal from 'sweetalert2';
import { TravelsService } from 'src/app/Services/travels.service';


@Component({
  selector: 'app-update-request',
  templateUrl: './update-request.component.html',
  styleUrls: ['./update-request.component.css']
})
export class UpdateRequestComponent implements OnInit {
  public lCity = [
    "ירושלים", "תל אביב-יפו", "חיפה", "ראשון לציון", "פתח תקווה", "נתניה", "אשדוד", "בני ברק", "באר שבע", "חולון", "רמת גן", "אשקלון", "רחובות", "בית שמש", "בת ים", "הרצליה", "כפר סבא", "חדרה", "מודיעין- מכבים- רעות", "לוד", "מודיעין עילית", "רעננה", "נצרת", "רמלה", "רהט", "ראש העין", "הוד השרון", "ביתר עילית", "גבעתיים", "נהריה", "קריית גת", "קריית אתא", "עפולה", "אום אל-פחם", "יבנה", "אילת", "נס ציונה", "אלעד", "עכו", "רמת השרון", "טבריה", "קריית מוצקין", "כרמיאל", "טייבה", "קריית ביאליק", "נוף הגליל", "שפרעם", "נתיבות", "קריית אונו", "קריית ים", "מעלה אדומים", "צפת", "אור יהודה", "דימונה", "טמרה", "אופקים", "סחנין", "שדרות", "באקה אל-גרבייה", "יהוד-מונוסון", "באר יעקב", "גבעת שמואל", "כפר יונה", "ערד", "טירה", "טירת כרמל", "עראבה", "מגדל העמק", "קריית מלאכי", "כפר קאסם", "יקנעם עילית", "קלנסווה", "נשר", "קריית שמונה", "מעלות-תרשיחא", "אור עקיבא", "אריאל", "בית שאן"
  ];
  person = new request();
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
  });


  constructor(private fb: FormBuilder, private requests: RequestsService, private router: Router, private route: ActivatedRoute,
    public travelSer: TravelsService) { }

  ngOnInit(): void {

    this.route.queryParams.subscribe(params => {

      var id = this.route.snapshot.params['id'];
      this.requests.getById(id).subscribe(x => {
        this.myForm = this.fb.group(x);
        this.myForm.patchValue({ date_time:new Date( new Date(x.date_time).setTime(new Date(x.date_time).getTime()  + (3*60*60*1000))).toISOString().substring(0, 16) })
      });
    });
  }
  onSubmit(): void {

    var r = <request>this.myForm.value;

    this.requests.updateRequest(r).subscribe(res => {
      if (res == null) {

        Swal.fire('', "ארעה שגיאה במערכת", 'error');
      }
      else {
        Swal.fire('', "בקשת הנסיעה עודכנה בהצלחה", 'success');
        this.router.navigateByUrl('privateArea/ActiveOffers');
      }

    }, err => { console.log("not good ") });

  }

  onClick(id: number): void {
    this.requests.ConnectDriver(id, this.person.id).subscribe(res => {

      console.log("Please wait! ")

    }, err => { console.log("Operation failed ") });



  }
}




