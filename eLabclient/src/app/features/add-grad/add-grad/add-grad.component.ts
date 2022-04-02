import { ConditionalExpr } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'app-add-grad',
  templateUrl: './add-grad.component.html',
  styleUrls: ['./add-grad.component.css']
})
export class AddGradComponent implements OnInit {

  addGradForm:FormGroup = new FormGroup({});
  constructor(public service:AppComponent, private formBuilder:FormBuilder, public route:Router) { }

  ngOnInit(): void {
    this.addGradForm = this.formBuilder.group({
      // 'id' : new FormControl('',[Validators.required]),
      'name': new FormControl('',[Validators.required]),
      'PTT': new FormControl('',[Validators.required])
    })
  }

  addGrad()
  {
    this.service.addGrad(this.addGradForm.value).subscribe(data => {
    }), err=> {
      console.log("Unable to add grad");
    }
    this.addGradForm.reset();
    this.route.navigate(['/showGrad']);
  }

}
