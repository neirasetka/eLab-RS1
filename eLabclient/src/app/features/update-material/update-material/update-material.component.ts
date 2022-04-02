import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { GetMaterijaliResponse } from 'src/app/responses/GetMaterijaliResponse';

@Component({
  selector: 'app-update-material',
  templateUrl: './update-material.component.html',
  styleUrls: ['./update-material.component.css']
})
export class UpdateMaterialComponent implements OnInit {

  updateMaterialForm:FormGroup = new FormGroup({});
  updateMaterialRequest : GetMaterijaliResponse;
  constructor(private formBuilder: FormBuilder, private service:AppComponent,private router:ActivatedRoute, public route:Router) {

    this.updateMaterialForm = this.formBuilder.group({
      name: new FormControl(''), 
      id: new FormControl(''), 
      description: new FormControl(''), 
      quantity: new FormControl('')
    })
   }

  ngOnInit(): void {

    this.service.getCurrentMaterialData(this.router.snapshot.params.id).subscribe((data)=>
    {
      this.updateMaterialForm = new FormGroup({
        name: new FormControl(data['name']),
        id: new FormControl(this.router.snapshot.params.id),
        description: new FormControl(data['description']), 
        quantity: new FormControl(data['quantity'])
      })
    })
  }

  updateMaterial(){
    this.service.updateMaterial(this.updateMaterialForm.value).subscribe(data=>{
    }), err=> {
      console.log("Unable to update material");
    }
    this.updateMaterialForm.reset();
    this.route.navigate(['/showMaterials']);
  }
}
