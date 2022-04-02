import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MaterialDetailsComponent } from './features/material-details/material-details/material-details.component';
import { ShowMaterialsComponent } from './features/show-materials/show-materials/show-materials.component';
import { AddMaterialComponent } from './features/add-material/add-material/add-material.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UpdateMaterialComponent } from './features/update-material/update-material/update-material.component';
import { HomeComponentComponent } from './features/home/home-component/home-component.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {AddGradComponent} from "./features/add-grad/add-grad/add-grad.component";
import {ShowGradComponent} from "./features/show-grad/show-grad/show-grad.component";
import {GradDetailsComponent} from "./features/grad-details/grad-details/grad-details.component";
import {UpdateGradComponent} from "./features/update-grad/update-grad/update-grad.component";

@NgModule({
  declarations: [
    AppComponent,
    MaterialDetailsComponent,
    ShowMaterialsComponent,
    AddMaterialComponent,
    UpdateMaterialComponent,
    HomeComponentComponent,
    AddGradComponent,
    ShowGradComponent,
    GradDetailsComponent,
    UpdateGradComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule, NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
