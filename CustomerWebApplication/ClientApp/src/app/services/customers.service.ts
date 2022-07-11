import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NameValue } from '../interfaces';

@Injectable({
  providedIn: 'root'
})
export class CustomersService {
  // private variables: NameValue[] = [];
  constructor(private http: HttpClient) { 
    console.log("CustomersService works!")
    this.get();
    
    
  }

  get =():Observable<any>  => {
    return this.http.get("https://localhost:7189/api/Customer");
  }

  gerAddresses = (id:number)=>{
    return this.http.get("https://localhost:7189/api/Adress/"+id);
  }

}
