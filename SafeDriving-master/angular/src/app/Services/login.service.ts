import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { person } from '../Module/person';
import { personInfo } from '../Module/personInfo';
//import {Http, Headers} from '@angular/http';


@Injectable({
  providedIn: 'root'
})


export class LoginService {
  url = "http://localhost:50940/api/person"

  userLogin = new Subject<string>();
  constructor(private http: HttpClient) { }

  signIn(person: person): Observable<personInfo> {
    return this.http.get<personInfo>(this.url + "?email=" + person.mail + "&password=" + person.password);
  }
  signUp(person: person): Observable<personInfo> {
    return this.http.post<personInfo>(this.url + '/createNewPerson', person);
  }
  getPersonStatus(): Observable<person[]> {
    return this.http.get<person[]>(this.url + '/getPersonStatus');
  }
  getPersonById(id): Observable<person[]> {
    return this.http.get<person[]>(this.url + '/GetPersonById?id=' + id);
  }

  updatePersonStatus(person: person): Observable<boolean> {
    return this.http.post<boolean>(this.url + '/updatePersonStatus', person);
  }
  updatePerson(person: person): Observable<boolean> {
    return this.http.post<boolean>(this.url + '/updatePerson', person);
  }
}