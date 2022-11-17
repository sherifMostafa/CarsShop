import { FeatureService } from './Services/feature.service';
import { MakeService } from './Services/make.service';
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
import { MultiSelectModule } from 'primeng/multiselect';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { VehicleService } from './Services/vehicle.service';
import { ListVehiclesComponent } from './Components/list-vehicles/list-vehicles.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgSelectModule } from '@ng-select/ng-select';

@NgModule({
  declarations: [
    AppComponent,
    VehicleFormComponent,
    NotFoundComponent,
    NavbarComponent,
    HomeComponent,
    ListVehiclesComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MultiSelectModule,
    NgMultiSelectDropDownModule.forRoot(),
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    NgSelectModule,
  ],
  providers: [MakeService, FeatureService, VehicleService],
  bootstrap: [AppComponent],
})
export class AppModule {}
