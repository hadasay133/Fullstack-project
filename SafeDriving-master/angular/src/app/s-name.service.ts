import { Injectable } from '@angular/core';
import{HttpClientModule,HttpParams, HttpClient}from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SNameService {

  constructor(private http:HttpClient) {
    
   }
   public getData = () =>{
    //return this.http.get(http://localhost:?/api/);
  }
}
