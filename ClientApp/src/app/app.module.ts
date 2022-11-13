import { FeatureSerice } from './Services/feature.service';
import { MakeSerice } from './Services/make.service';
import { AppRoutingModule } from './app-routing.module';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { VehicleFormComponent } from './Components/vehicle-form/vehicle-form.component';
import { NotFoundComponent } from './Components/not-found/not-found.component';
import { NavbarComponent } from './Components/navbar/navbar.component';
import { HomeComponent } from './Components/home/home.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    VehicleFormComponent,
    NotFoundComponent,
    NavbarComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [MakeSerice, FeatureSerice],
  bootstrap: [AppComponent],
})
export class AppModule {}
