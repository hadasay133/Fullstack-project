import { Component, OnInit } from '@angular/core';
import { LoginService } from '../Services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  isLogin = false;
  name = '';
  constructor(private loginSer: LoginService, private router: Router) { }

  ngOnInit(): void {
    if (localStorage.getItem('id')) {
      this.name = localStorage.getItem('name');
      this.isLogin = true;
    }

    this.loginSer.userLogin.subscribe(x => {
      this.name = localStorage.getItem('name');
      this.isLogin = true;
    })
  }
  logout() {
    this.isLogin = false;
    localStorage.removeItem('id');
    localStorage.removeItem('name');
    localStorage.removeItem('tz');
    localStorage.removeItem('is_manager');
       
    this.router.navigateByUrl('about');

  }
}
