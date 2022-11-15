import { Component, OnInit } from '@angular/core';
import { VehicleService } from 'src/app/Services/vehicle.service';

@Component({
  selector: 'app-list-vehicles',
  templateUrl: './list-vehicles.component.html',
  styleUrls: ['./list-vehicles.component.css'],
})
export class ListVehiclesComponent implements OnInit {
  constructor(private vehicleService: VehicleService) {}

  ngOnInit(): void {}
}
