import { Component, OnInit } from '@angular/core';
import { request } from '../Module/request';
import { travel } from '../Module/Travels';
import { Offer } from '../Module/offer';
//import { Route } from '@angular/compiler/src/core';
import { OffersService } from '../Services/offers.service';
import { RequestsService } from '../Services/requests.service';
import { TravelsService } from '../Services/travels.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-private-area',
  templateUrl: './private-area.component.html',
  styleUrls: ['./private-area.component.css']
})
export class PrivateAreaComponent implements OnInit {
  isManager = false;
  id: number;
  travelList: travel[] = null;
  offerList: Offer[] = null;
  requestList: request[] = null;
  constructor(private offers: OffersService, private requests: RequestsService
    , private travels: TravelsService, private router: Router) { }

  ngOnInit(): void {
    this.isManager = (localStorage.getItem('is_manager')  && localStorage.getItem('is_manager').toString()) === "true";


    if (localStorage.getItem('id') == null) {
      Swal.fire('', '"אינך מחובר, הנך מועבר לדף הכניסה"', 'error');
      this.router.navigate(['signInForm']);

    }
    else {
      this.id = JSON.parse(localStorage.getItem('id'));
     
    }

  }

  
}
