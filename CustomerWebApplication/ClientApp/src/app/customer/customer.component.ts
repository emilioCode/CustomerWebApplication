import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { CustomersService } from '../services/index';
@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {

  constructor(private customerService: CustomersService , private formBuilder: FormBuilder, private http: HttpClient) { 
    this.form = this.formBuilder.group({
      Id: [0],
      Name: ['', Validators.required],
      Phone: [''],
      Gender: ['', Validators.required]
    });
  }
  customers$!: Observable<any>;
  addresses$!: Observable<any>;
  option!:string;
  costumer:any={};
  modal_title:string = "";
  form: FormGroup;
  headers = new HttpHeaders().set("content-type", "application/json");
  option2:string = '';
  address: any= {
    Id: 0,
    Address0: "",
    Type0: "",
    Customerid: 0
  }

  ngOnInit(): void {
    this.customers$ = this.customerService.get();


  }
  genders: string[] = [
    'M',
    'F'
  ];

  fillModal(option='create',object: any){
    this.option = option;
    switch (option) {
      case 'create':
        this.costumer = {
          Id: 0,
          Name: "",
          Phone: "",
          Gender: "M"
        };
        this.modal_title= "New Customer";
        var { Id, Name, Phone,Gender } = this.form.controls;
        Id.setValue(0);
        Name.setValue("");
        Phone.setValue("");
        Gender.setValue("");
        break;
      case 'edit':
        this.costumer = object;
        this.modal_title= "Customer";
        var { Id, Name, Phone,Gender } = this.form.controls;
        Id.setValue(this.costumer.Id);
        Name.setValue(this.costumer.Name);
        Phone.setValue(this.costumer.Phone);
        Gender.setValue(this.costumer.Gender);
        break;
      case 'delete':
        this.costumer = object;
        this.modal_title= "Are you sure?";
        var { Id, Name, Phone,Gender } = this.form.controls;
        Id.setValue(this.costumer.Id);
        Name.setValue(this.costumer.Name);
        Phone.setValue(this.costumer.Phone);
        Gender.setValue(this.costumer.Gender);
        break;
      default:
        break;
    }
    this.addresses$ = this.customerService.getAddresses(this.costumer.Id);
  }

  fillModal2(option='create', object: any){
    this.option2 = option;
    const { Id } = this.form.controls;
    switch (option) {
      case 'create':
        this.initAddress();
        break;
      case 'edit':
        this.address = object;
        break;
      case 'delete':
        this.address = object;
        break;
      default:
        break;
    }
    console.log(this.address)
  }

  submit(){
    const customer = {
      Id: this.form.value.Id,
      Name: this.form.value.Name,
      Phone: this.form.value.Phone,
      Gender: this.form.value.Gender,
    }
    const request = {
      operation: this.option,
      stringify: JSON.stringify(customer)
    }

    this.http.post(this.customerService.customer_service,request,{headers:this.headers,responseType:'json'})
      .subscribe(res=>{
        document.getElementById('btnClose')?.click();
        this.customers$ = this.customerService.get();
      },error => {
        console.error(error);
      });
  }

  submitAddress(){
    const request = {
      operation: this.option2,
      stringify: JSON.stringify(this.address)
    }
    console.log(request)
    this.http.post(this.customerService.address_service,request,{headers:this.headers,responseType:'json'})
    .subscribe(res=>{
      console.log(res);
      this.addresses$ = this.customerService.getAddresses(this.address.Customerid);
      this.resetAddress()

    },error => {
      console.error(error);
    });

  }

  initAddress(){
    this.address = {
      Id: 0,
      Address0: "",
      Type0: "",
      Customerid: 0
    }
  }

  resetAddress(){
    this.option2 = '';
    this.initAddress();
  }
}
