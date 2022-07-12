import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NameValue } from '../interfaces';

@Injectable({
  providedIn: 'root'
})
export class CustomersService {
  public customer_service = "https://localhost:7189/api/Customer";
  public address_service = "https://localhost:7189/api/Address";
  
  constructor(private http: HttpClient) { 
    console.log("CustomersService works!")
    this.get();
    
    
  }

  get =():Observable<any>  => {
    return this.http.get(this.customer_service);
  }

  getAddresses = (id:number)=>{
    return this.http.get(this.address_service + "/" + id);
  }

}
