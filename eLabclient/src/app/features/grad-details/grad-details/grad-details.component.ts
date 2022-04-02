import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { GetMaterijaliResponse } from 'src/app/responses/GetMaterijaliResponse';
import {GetGradResponse} from "../../../responses/GetGradResponse";

@Component({
  selector: 'app-grad-details',
  templateUrl: './grad-details.component.html',
  styleUrls: ['./grad-details.component.css']
})
export class GradDetailsComponent implements OnInit {

  gradovi: GetGradResponse[];
  constructor(public service:AppComponent,public route:Router) { }

  ngOnInit(): void {
    this.loadGradovi();
  }

  loadGradovi(){
    this.service.getGrad().subscribe(response => {
      this.gradovi = response;
    })
  }

  deleteGrad(item)
  {
    this.service.deleteGrad(item).subscribe(data=>
      {
         this.loadGradovi();

      })
  }

}
