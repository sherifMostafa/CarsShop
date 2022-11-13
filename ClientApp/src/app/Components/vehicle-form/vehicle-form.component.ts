import { KeyValuePair } from './../../Models/KeyValuePairModel';
import { MakeSerice } from './../../Services/make.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FeatureSerice } from 'src/app/Services/feature.service';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css'],
})
export class VehicleFormComponent implements OnInit {
  makes: any[];
  features: any[];
  submitted = false;
  models: any;
  makeId: any;
  vehicle: any;
  dataForm: FormGroup;

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
    });
  }

  loadData() {
    this.makeService.getMakes().subscribe((data) => {
      this.makes = data;
    });

    this.featureService.getFeatures().subscribe((data) => {
      this.features = data;
    });
  }

  save() {
    this.submitted = true;
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
