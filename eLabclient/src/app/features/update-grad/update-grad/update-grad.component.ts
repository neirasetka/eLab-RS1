import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { GetMaterijaliResponse } from 'src/app/responses/GetMaterijaliResponse';

@Component({
  selector: 'app-update-grad',
  templateUrl: './update-grad.component.html',
  styleUrls: ['./update-grad.component.css']
})
export class UpdateGradComponent implements OnInit {

  updateGradForm:FormGroup = new FormGroup({});
  updateGradRequest : GetMaterijaliResponse;
  constructor(private formBuilder: FormBuilder, private service:AppComponent,private router:ActivatedRoute, public route:Router) {

    this.updateGradForm = this.formBuilder.group({
      name: new FormControl(''),
      id: new FormControl(''),
      description: new FormControl(''),
      quantity: new FormControl('')
    })
   }

  ngOnInit(): void {

    this.service.getCurrentGradData(this.router.snapshot.params.id).subscribe((data)=>
    {
      this.updateGradForm = new FormGroup({
        name: new FormControl(data['name']),
        id: new FormControl(this.router.snapshot.params.id),
        description: new FormControl(data['description']),
        quantity: new FormControl(data['quantity'])
      })
    })
  }

  updateGrad(){
    this.service.updateGrad(this.updateGradForm.value).subscribe(data=>{
    }), err=> {
      console.log("Unable to update grad");
    }
    this.updateGradForm.reset();
    this.route.navigate(['/showGrads']);
  }
}
