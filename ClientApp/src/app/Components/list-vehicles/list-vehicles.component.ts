import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MakeService } from 'src/app/Services/make.service';
import { ModelService } from 'src/app/Services/model.service';
import { VehicleService } from 'src/app/Services/vehicle.service';

@Component({
  selector: 'app-list-vehicles',
  templateUrl: './list-vehicles.component.html',
  styleUrls: ['./list-vehicles.component.css'],
})
export class ListVehiclesComponent implements OnInit {
  private readonly PAGE_SIZE = 10;

  constructor(
    private vehicleService: VehicleService,
    private makeService: MakeService,
    private modelService: ModelService,
    private toastr: ToastrService
  ) {}

  queryResult: any;
  vehicleList: any[];
  makes: any[];
  models: any[];
  filter: any = {
    pageSize: 10,
  };
  columns = [
    { title: 'Id' },
    // { title: 'Name', key: 'name', isSortable: false },
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
    { title: 'ContactName', key: 'contactName', isSortable: true },
  ];

  ngOnInit(): void {
    this.loadMakes();
    this.loadModels();
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
    this.filter = {
      page: 1,
      pageSize: this.PAGE_SIZE,
    };
    this.onFilterChange();
  }

  private populateVehicles() {
    this.vehicleService.get(this.filter).subscribe({
      next: (result: any) => {
        this.queryResult = result;
      },
      complete: () => {
        console.log(this.vehicleList);
      },
    });
  }

  loadMakes() {
    this.makeService.getMakes().subscribe((data) => {
      this.makes = data;
    });
  }
  loadModels() {
    this.modelService.getModels().subscribe((data) => {
      this.models = data;
    });
  }

  sortBy(columnName: any) {
    if (this.filter.sortBy === columnName)
      this.filter.isSortAscending = !this.filter.isSortAscending;
    else {
      this.filter.sortBy = columnName;
      this.filter.isSortAscending = true;
    }

    this.populateVehicles();
  }

  onPageChange(page: any) {
    this.filter.page = page;
    this.populateVehicles();
  }

  showSuccess(message: string, subject: string) {
    this.toastr.success(message, subject);
  }
}
