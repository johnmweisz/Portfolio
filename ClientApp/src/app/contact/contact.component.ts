import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {

  public showContact: boolean;
  public showNumber: boolean;
  public displayNumber: string;
  public Name: string;
  public Email: string;
  public Phone: string;
  public Message: string;

  constructor(private _http: HttpClient) { }

  ngOnInit() {
    this.showContact = true;
    this.showNumber = true;
  }

  inquire() {
    const contactform = {
      Name: this.Name,
      Email: this.Email,
      Phone: this.Phone,
      Message: this.Message
    };
    return this._http.post('./Home/Inquire', contactform, { headers: new HttpHeaders({ 'Angular': 'secretkeylol'})})
    .subscribe(
      res => this.showContact = !this.showContact,
      err => console.log(err)
    );
  }

  getNumber() {
    if (this.showNumber) {
      this.showNumber = !this.showNumber;
      return this._http.get('./Home/GetNumber', { headers: new HttpHeaders({ 'Angular': 'secretkeylol'})})
      .subscribe(
        res => this.displayNumber = <string>res,
        err => console.log(err)
      );
    }
  }

  hideNumber() {
    this.displayNumber = null;
    this.showNumber = !this.showNumber;
  }

}
