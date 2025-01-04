import { Component, OnInit } from '@angular/core';
import { Offer } from 'src/app/Module/offer';
import { OffersService } from 'src/app/Services/offers.service';
import { TravelsService } from 'src/app/Services/travels.service';


@Component({
  selector: 'app-all-offers',
  templateUrl: './all-offers.component.html',
  styleUrls: ['./all-offers.component.css']
})
export class AllOffersComponent implements OnInit {
  offers: Offer[] = [];
  offerSource: Offer[] = [];
  dateFilter: Date ;
  today = new Date();
  selectedSourceCity = '';
  selectedDestCity = '';
  public lCity = [
    "ירושלים", "תל אביב-יפו", "חיפה", "ראשון לציון", "פתח תקווה", "נתניה", "אשדוד", "בני ברק", "באר שבע", "חולון", "רמת גן", "אשקלון", "רחובות", "בית שמש", "בת ים", "הרצליה", "כפר סבא", "חדרה", "מודיעין- מכבים- רעות", "לוד", "מודיעין עילית", "רעננה", "נצרת", "רמלה", "רהט", "ראש העין", "הוד השרון", "ביתר עילית", "גבעתיים", "נהריה", "קריית גת", "קריית אתא", "עפולה", "אום אל-פחם", "יבנה", "אילת", "נס ציונה", "אלעד", "עכו", "רמת השרון", "טבריה", "קריית מוצקין", "כרמיאל", "טייבה", "קריית ביאליק", "נוף הגליל", "שפרעם", "נתיבות", "קריית אונו", "קריית ים", "מעלה אדומים", "צפת", "אור יהודה", "דימונה", "טמרה", "אופקים", "סחנין", "שדרות", "באקה אל-גרבייה", "יהוד-מונוסון", "באר יעקב", "גבעת שמואל", "כפר יונה", "ערד", "טירה", "טירת כרמל", "עראבה", "מגדל העמק", "קריית מלאכי", "כפר קאסם", "יקנעם עילית", "קלנסווה", "נשר", "קריית שמונה", "מעלות-תרשיחא", "אור עקיבא", "אריאל", "בית שאן"
  ];
  personId: number;
  constructor(private offerService: OffersService, public travelSer: TravelsService) { }
  filter() {
    this.offers = [...this.offerSource];
    if (this.selectedDestCity && this.selectedDestCity != '') {
      this.offers = this.offers.filter(x => x.destination_city === this.selectedDestCity);
    }
    if (this.selectedSourceCity && this.selectedSourceCity != '') {
      this.offers = this.offers.filter(x => x.resourse_city === this.selectedSourceCity);
    }
    if (this.dateFilter) {
      this.offers = this.offers.filter(x => new Date(x.date_time).getMonth() === new Date(this.dateFilter).getMonth()
        && new Date(x.date_time).getFullYear() ===  new Date(this.dateFilter).getFullYear()
        && new Date(x.date_time).getDate() ===  new Date(this.dateFilter).getDate()

      );
    }
  }
  clear() {
    this.dateFilter = null;
    this.selectedSourceCity = '';
    this.selectedDestCity = '';
    this.offers = [...this.offerSource];
  }
  ngOnInit(): void {
    this.personId = +localStorage.getItem('id');
    this.getPersonOffers();
  }

  getPersonOffers() {
    this.offerService.getAllActiveOffers().subscribe(x => {
      this.offers = x;
      this.offerSource = [...x];
    });

  }

}
