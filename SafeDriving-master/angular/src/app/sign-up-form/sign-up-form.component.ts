import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { person } from '../Module/person';
import { LoginService } from '../Services/login.service';
import { Router } from '@angular/router';
import { ErrorTypes } from '../Module/personInfo';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-sign-up-form',
  templateUrl: './sign-up-form.component.html',
  styleUrls: ['./sign-up-form.component.css']
})
export class SignUpFormComponent implements OnInit {
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
      Validators.required,
      Validators.pattern(
        '(?=.*[A-Za-z0-9]).{8,}'
      ),
    ]),
  });

  constructor(private login: LoginService, private router: Router) { }
  //
  message: string = "";//להציג את ההודעה
  ngOnInit(): void {
  }
  onSubmit(): void {

    this.person.tz = this.myForm.value.tz;
    this.person.adress = this.myForm.value.adress;
    this.person.mail = this.myForm.value.mail;
    this.person.phone = this.myForm.value.phone;
    this.person.username = this.myForm.value.username;
    this.person.inqure = this.myForm.value.inqure;
    this.person.password = this.myForm.value.password;
    this.person.ok = true;
    this.person.is_manager = false;

    this.login.signUp(this.person).subscribe(res => {
      if (res.Status) {
        this.person = res.Person;//לכאורה זה מה שיש וזה מיותר
        localStorage.setItem('id', JSON.stringify(res.Person.id));
        localStorage.setItem('name', res.Person.username);
        localStorage.setItem('tz', JSON.stringify(res.Person.tz));
     
        this.login.userLogin.next(res.Person.username);
        this.router.navigate(['privateArea']);
      } else {
        Swal.fire('',"כתובת המייל קימת במאגר",'error')
        this.router.navigate(['signInForm']);
      }
    } 
      , err => {
        Swal.fire('',"שגיאת שרת",'error')
 
      });
  }


  new(): void {
    this.router.navigate(['signInForm']);
  }
  reset(): void {
    this.myForm.reset('',);
  }
}