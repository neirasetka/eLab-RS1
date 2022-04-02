import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { GetMaterijaliResponse } from 'src/app/responses/GetMaterijaliResponse';

@Component({
  selector: 'app-material-details',
  templateUrl: './material-details.component.html',
  styleUrls: ['./material-details.component.css']
})
export class MaterialDetailsComponent implements OnInit {

  materijali: GetMaterijaliResponse[];
  constructor(public service:AppComponent,public route:Router) { }

  ngOnInit(): void {
    this.loadMaterials();
  }

  loadMaterials(){
    this.service.getMaterials().subscribe(response => {
      this.materijali = response;
    })
  }

  deleteCategory(item)
  {
    this.service.deleteMaterial(item).subscribe(data=>
      {
         this.loadMaterials();
        
      })
  }

}
