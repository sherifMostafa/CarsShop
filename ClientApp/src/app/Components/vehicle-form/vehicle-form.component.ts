import { KeyValuePair } from './../../Models/KeyValuePairModel';
import { MakeSerice } from './../../Services/make.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FeatureSerice } from 'src/app/Services/feature.service';
import { VehicleModel } from 'src/app/Models/VehicleModel';
import { IDropdownSettings } from 'ng-multiselect-dropdown';

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
  featureIds: number[] = [];

  get frm() {
    return this.dataForm.controls;
  }
  get fg() {
    return this.dataForm;
  }
  constructor(
    private makeService: MakeSerice,
    private featureService: FeatureSerice
  ) {}

  ngOnInit(): void {
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
    this.model.features = this.featureIds.map((x) => x);
    console.log(this.model.features);
    if (this.dataForm.invalid) {
      return;
    }
  }

  onModelChange(e: any) {
    console.log(e.target.value);

    var selectedMake = this.makes.find((m) => m.id == e.target.value);
    this.models = selectedMake ? selectedMake.models : [];
  }
}
