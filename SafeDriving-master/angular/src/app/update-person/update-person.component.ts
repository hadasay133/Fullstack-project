import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { person } from '../Module/person';
import { LoginService } from '../Services/login.service';
import { Router } from '@angular/router';
import { ErrorTypes } from '../Module/personInfo';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-update-person',
  templateUrl: './update-person.component.html',
  styleUrls: ['./update-person.component.css']
})
export class UpdatePersonComponent implements OnInit {
  person = new person();
  myForm: FormGroup = new FormGroup({
    username: new FormControl(),
    tz: new FormControl(),
    adress: new FormControl(),
    mail: new FormControl(),
    phone: new FormControl(),
    inqure: new FormControl(),
    is_manager: new FormControl(0),
    password: new FormControl('', [
      Validators.pattern(
        '(?=.*[A-Za-z0-9]).{8,}'
      ),
    ]),
  });

  constructor(private fb: FormBuilder, private login: LoginService, private router: Router) { }
  //
  personId = 0;
  message: string = "";//להציג את ההודעה
  ngOnInit(): void {

    this.personId = +localStorage.getItem('id');
    this.login.getPersonById(this.personId).subscribe((x) => {
      this.myForm = this.fb.group(x);
    });
  }
  onSubmit(): void {

    var p = <person>this.myForm.value;
    p.ok = true;
    p.is_manager = false;

    this.login.updatePerson(p).subscribe(res => {
      if (res) {

        Swal.fire('', "השמירה בוצעה בהצלחה", 'success');
        localStorage.setItem('name', p.username);
        this.login.userLogin.next();
        this.router.navigate(['privateArea/']);
      }
    }
      , err => {
        Swal.fire('', "שגיאת שרת", 'error')

      });
  }


}