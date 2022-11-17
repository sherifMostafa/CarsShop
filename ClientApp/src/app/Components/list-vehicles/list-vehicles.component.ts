import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { VehicleService } from 'src/app/Services/vehicle.service';

@Component({
  selector: 'app-list-vehicles',
  templateUrl: './list-vehicles.component.html',
  styleUrls: ['./list-vehicles.component.css'],
})
export class ListVehiclesComponent implements OnInit {
  constructor(
    private vehicleService: VehicleService,
    private toastr: ToastrService
  ) {}

  vehicleList: any[];

  ngOnInit(): void {
    this.loadVehicles();
    console.log(this.vehicleList);
  }

  loadVehicles() {
    this.vehicleService.get().subscribe({
      next: (d) => {
        this.vehicleList = d;
        console.log(this.vehicleList);
      },
      error: (err) => {
        console.log(err.message);
      },
      complete: () => {
        this.showSuccess('data Ready', 'List');
      },
    });
  }

  showSuccess(message: string, subject: string) {
    this.toastr.success(message, subject);
  }
}
