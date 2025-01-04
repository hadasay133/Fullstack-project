import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { travel } from '../Module/Travels';

@Injectable({
  providedIn: 'root'
})
export class TravelsService {
  url = "http://localhost:50940/api/travel/";

  constructor(private http: HttpClient) { }

  getByPersonId(id: number): Observable<travel[]> {//יש הבדל אם חוזר list או arr?
    //debugger;
    return this.http.get<travel[]>(this.url + "getByPersonId/" + "?tz=" + id);

  }
}





