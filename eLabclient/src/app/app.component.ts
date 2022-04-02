import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Guid } from 'guid-typescript';
import { GetMaterijaliResponse } from './responses/GetMaterijaliResponse';
import {GetGradResponse} from "./responses/GetGradResponse";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'eLabclient';



  constructor(private http: HttpClient)
  {

  }

  getMaterials()
  {
    return this.http.get<GetMaterijaliResponse[]>('https://localhost:5001/ApiMaterijali');
  }

  getGrad()
  {
    return this.http.get<GetGradResponse[]>('https://localhost:5001/ApiGrad');
  }

  deleteMaterial(materijalId:Guid)
  {
     return this.http.delete('https://localhost:5001/ApiMaterijali/'+ materijalId);
  }

  deleteGrad(gradId:Guid)
  {
     return this.http.delete('https://localhost:5001/ApiGrad/'+ gradId);
  }

  addMaterial(model:any)
  {
    return this.http.post('https://localhost:5001/ApiMaterijali/',model);
  }

  addGrad(model:any)
  {
    return this.http.post('https://localhost:5001/ApiGrad/',model);
  }


  updateMaterial(model:GetMaterijaliResponse)
  {
    return this.http.put<GetMaterijaliResponse>('https://localhost:5001/ApiMaterijali/',model);
  }

  getCurrentMaterialData(materijalId:number)
  {
    return this.http.get<GetMaterijaliResponse>('https://localhost:5001/ApiMaterijali/'+ materijalId);
  }

  updateGrad(model:GetGradResponse)
  {
    return this.http.put<GetGradResponse>('https://localhost:5001/ApiGrad/',model);
  }

  getCurrentGradData(gradId:number)
  {
    return this.http.get<GetGradResponse>('https://localhost:5001/ApiGrad/'+ gradId);
  }

}
