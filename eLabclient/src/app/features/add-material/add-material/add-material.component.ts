import { ConditionalExpr } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'app-add-material',
  templateUrl: './add-material.component.html',
  styleUrls: ['./add-material.component.css']
})
export class AddMaterialComponent implements OnInit {

  addMaterialForm:FormGroup = new FormGroup({});
  constructor(public service:AppComponent, private formBuilder:FormBuilder, public route:Router) { }

  ngOnInit(): void {
    this.addMaterialForm = this.formBuilder.group({
      // 'id' : new FormControl('',[Validators.required]),
      'name': new FormControl('',[Validators.required]), 
      'description': new FormControl('',[Validators.required]), 
      'quantity': new FormControl('',[Validators.required])
    })
  }

  addMaterial()
  {
    this.service.addMaterial(this.addMaterialForm.value).subscribe(data => {
    }), err=> {
      console.log("Unable to add material");
    }
    this.addMaterialForm.reset();
    this.route.navigate(['/showMaterials']);
  }

}
