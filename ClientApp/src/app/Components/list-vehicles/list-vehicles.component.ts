import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MakeService } from 'src/app/Services/make.service';
import { VehicleService } from 'src/app/Services/vehicle.service';

@Component({
  selector: 'app-list-vehicles',
  templateUrl: './list-vehicles.component.html',
  styleUrls: ['./list-vehicles.component.css'],
})
export class ListVehiclesComponent implements OnInit {
  constructor(
    private vehicleService: VehicleService,
    private makeService: MakeService,
    private toastr: ToastrService
  ) {}

  vehicleList: any[];
  makes: any[];
  filter: any = {};

  ngOnInit(): void {
    this.loadMakes();
    this.populateVehicles();
  }

  // loadVehicles() {
  //   this.vehicleService.get().subscribe({
  //     next: (d) => {
  //       this.vehicleList = d;
  //       console.log(this.vehicleList);
  //     },
  //     error: (err) => {
  //       console.log(err.message);
  //     },
  //     complete: () => {
  //       this.showSuccess('data Ready', 'List');
  //     },
  //   });
  // }

  onFilterChange() {
    this.populateVehicles();
  }

  resetFilter() {
    this.filter = {};
    this.onFilterChange();
  }

  private populateVehicles() {
    this.vehicleService.get(this.filter).subscribe({
      next: (d) => {
        this.vehicleList = d;
      },
    });
  }

  loadMakes() {
    this.makeService.getMakes().subscribe((data) => {
      this.makes = data;
    });
  }

  onFiterChange(e: any) {
    if (this.filter.makeid) console.log(e);
  }

  showSuccess(message: string, subject: string) {
    this.toastr.success(message, subject);
  }
}
