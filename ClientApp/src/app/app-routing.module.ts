import { HomeComponent } from './Components/home/home.component';
import { VehicleFormComponent } from './Components/vehicle-form/vehicle-form.component';
import { NotFoundComponent } from './Components/not-found/not-found.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListVehiclesComponent } from './Components/list-vehicles/list-vehicles.component';
const routes: Routes = [
  //   { path: "", component: AcademicDegreesListComponent },
  //   { path: "create", component: AcademicDegreesCreateComponent },
  //   { path: "update/:id", component: AcademicDegreesCreateComponent }
  { path: 'vehicles/new', component: VehicleFormComponent },
  { path: 'vehicles/update/:id', component: VehicleFormComponent },
  { path: 'vehicles', component: ListVehiclesComponent },
  { path: 'home', component: HomeComponent },
  // { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
