import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddMaterialComponent } from './features/add-material/add-material/add-material.component';
import { HomeComponentComponent } from './features/home/home-component/home-component.component';
import { MaterialDetailsComponent } from './features/material-details/material-details/material-details.component';
import { ShowMaterialsComponent } from './features/show-materials/show-materials/show-materials.component';
import { UpdateMaterialComponent } from './features/update-material/update-material/update-material.component';
import {AddGradComponent} from "./features/add-grad/add-grad/add-grad.component";
import {ShowGradComponent} from "./features/show-grad/show-grad/show-grad.component";
import {UpdateGradComponent} from "./features/update-grad/update-grad/update-grad.component";

const routes: Routes = [


  {path:'showMaterials', component:ShowMaterialsComponent},
  {path:'addnewMaterial',component:AddMaterialComponent},
  {path: 'updateMaterijal/:id', component:UpdateMaterialComponent},
  {path: 'updateGrad/:id', component:UpdateGradComponent},
  {path: 'addnewGrad', component: AddGradComponent},
  {path: 'showGrad', component: ShowGradComponent},
  {path: '', component:HomeComponentComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
