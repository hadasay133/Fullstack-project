import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2';
import { person } from '../Module/person';
import { LoginService } from '../Services/login.service';
import { OffersService } from '../Services/offers.service';

@Component({
  selector: 'app-set-person-status',
  templateUrl: './set-person-status.component.html',
  styleUrls: ['./set-person-status.component.css']
})
export class SetPersonStatusComponent implements OnInit {
  person: person[] = [];
  constructor(private perService: LoginService) { }
  personId = 0;
  ngOnInit(): void {
    this.personId = +localStorage.getItem('id');
    this.getPersonStatus();
  }

  getPersonStatus() {
    this.perService.getPersonStatus().subscribe(x => {
      this.person = x;
    });


  }
  updatePerson(per: person, isActive) {
    per.ok = isActive;
    this.perService.updatePerson(per).subscribe(x => {
      this.getPersonStatus();
      Swal.fire('', 'השמירה בוצעה בהצלחה', 'success');
    });

  }

}
