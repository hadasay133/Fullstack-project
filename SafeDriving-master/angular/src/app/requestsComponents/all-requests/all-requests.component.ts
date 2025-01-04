import { Component, OnInit } from '@angular/core';
import { request } from 'src/app/Module/request';
import { RequestsService } from 'src/app/Services/requests.service';

@Component({
  selector: 'app-all-requests',
  templateUrl: './all-requests.component.html',
  styleUrls: ['./all-requests.component.css']
})
export class AllRequestsComponent implements OnInit {
  requests: request[] = [];
  requestSource: request[] = [];
  dateFilter: Date = new Date();
  selectedSourceCity = '';
  selectedDestCity = '';
  public lCity = [
    "ירושלים", "תל אביב-יפו", "חיפה", "ראשון לציון", "פתח תקווה", "נתניה", "אשדוד", "בני ברק", "באר שבע", "חולון", "רמת גן", "אשקלון", "רחובות", "בית שמש", "בת ים", "הרצליה", "כפר סבא", "חדרה", "מודיעין- מכבים- רעות", "לוד", "מודיעין עילית", "רעננה", "נצרת", "רמלה", "רהט", "ראש העין", "הוד השרון", "ביתר עילית", "גבעתיים", "נהריה", "קריית גת", "קריית אתא", "עפולה", "אום אל-פחם", "יבנה", "אילת", "נס ציונה", "אלעד", "עכו", "רמת השרון", "טבריה", "קריית מוצקין", "כרמיאל", "טייבה", "קריית ביאליק", "נוף הגליל", "שפרעם", "נתיבות", "קריית אונו", "קריית ים", "מעלה אדומים", "צפת", "אור יהודה", "דימונה", "טמרה", "אופקים", "סחנין", "שדרות", "באקה אל-גרבייה", "יהוד-מונוסון", "באר יעקב", "גבעת שמואל", "כפר יונה", "ערד", "טירה", "טירת כרמל", "עראבה", "מגדל העמק", "קריית מלאכי", "כפר קאסם", "יקנעם עילית", "קלנסווה", "נשר", "קריית שמונה", "מעלות-תרשיחא", "אור עקיבא", "אריאל", "בית שאן"
  ];
  personId: number;
  constructor(private requestService: RequestsService) { }
  filter() {
    this.requests = [...this.requestSource];
    if (this.selectedDestCity && this.selectedDestCity != '') {
      this.requests = this.requests.filter(x => x.destination_city === this.selectedDestCity);
    }
    if (this.selectedSourceCity && this.selectedSourceCity != '') {
      this.requests = this.requests.filter(x => x.resourse_city === this.selectedSourceCity);
    }
    if (this.dateFilter) {
      this.requests = this.requests.filter(x => new Date(x.date_time).getMonth() ===  new Date(this.dateFilter).getMonth()
        && new Date(x.date_time).getFullYear() ===  new Date(this.dateFilter).getFullYear()
        && new Date(x.date_time).getDate() ===  new Date(this.dateFilter).getDate()

      );
    }
  }
  clear() {
    this.dateFilter = new Date();
    this.selectedSourceCity = '';
    this.selectedDestCity = '';
    this.requests = [...this.requestSource];
  }
  ngOnInit(): void {
    this.personId = +localStorage.getItem('id');
    this.getPersonRequests();
  }

  getPersonRequests() {
    this.requestService.getAllActiveRequests().subscribe(x => {
      this.requests = x;
      this.requestSource = [...x];
    });

  }

}
