import { KeyValuePair } from './../../Models/KeyValuePairModel';
import { MakeService } from './../../Services/make.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FeatureService } from 'src/app/Services/feature.service';
import { VehicleService } from 'src/app/Services/vehicle.service';
import { VehicleModel } from 'src/app/Models/VehicleModel';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css'],
})
export class VehicleFormComponent implements OnInit {
  dropdownSettings: IDropdownSettings;
  makes: any[];
  model: VehicleModel;
  features: any[];
  submitted = false;
  models: any;
  makeId: any;
  vehicle: any;
  dataForm: FormGroup;
  featureIds: Array<number> = new Array<number>();

  get frm() {
    return this.dataForm.controls;
  }
  get fg() {
    return this.dataForm;
  }
  constructor(
    private makeService: MakeService,
    private featureService: FeatureService,
    private vehicleService: VehicleService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.showSuccess();
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'name',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true,
    };
    this.model = new VehicleModel();
    this.buildform();

    this.loadData();
  }

  buildform() {
    this.dataForm = new FormGroup({
      nametxt: new FormControl('', Validators.compose([Validators.required])),
      model: new FormControl('', Validators.compose([Validators.required])),
      make: new FormControl('', Validators.compose([Validators.required])),
      isregisterd: new FormControl(
        '',
        Validators.compose([Validators.required])
      ),
      featuresControl: new FormControl(
        '',
        Validators.compose([Validators.required])
      ),
      contactName: new FormControl(
        '',
        Validators.compose([Validators.required])
      ),
      contactEmail: new FormControl(
        '',
        Validators.compose([Validators.required, Validators.email])
      ),
      contactPhone: new FormControl(
        '',
        Validators.compose([Validators.required])
      ),
    });
  }

  loadData() {
    this.makeService.getMakes().subscribe((data) => {
      this.makes = data;
    });

    this.featureService.getFeatures().subscribe((data) => {
      this.features = data;
      console.log(this.features);
    });
  }

  save() {
    this.submitted = true;

    console.log(this.model.features);
    if (this.dataForm.invalid) {
      return;
    }

    console.log(this.model);

    this.vehicleService.post(this.model).subscribe({
      next: () => this.resetForm,
      error: (d) => console.log(d),
      complete: () => this.showSuccess(),
    });
  }

  resetForm() {
    this.submitted = false;
    this.dataForm.reset();
    this.model = new VehicleModel();
  }

  onModelChange(e: any) {
    console.log(e.target.value);

    var selectedMake = this.makes.find((m) => m.id == e.target.value);
    this.models = selectedMake ? selectedMake.models : [];
  }

  onItemSelect(item: any) {
    this.featureIds.push(parseInt(item.id));
    console.log(this.featureIds);
    this.model.features = [...this.featureIds];
  }

  onItemDeSelect(item: any) {
    console.log('onItemDeSelect', item);
  }
  onSelectAll(items: any) {
    console.log('onSelectAll', items);
  }
  onUnSelectAll() {
    console.log('onUnSelectAll fires');
  }

  showSuccess() {
    this.toastr.success('Hello world!', 'Toastr fun!');
  }
}
