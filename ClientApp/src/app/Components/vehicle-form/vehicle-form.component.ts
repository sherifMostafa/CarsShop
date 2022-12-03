import { KeyValuePair } from './../../Models/KeyValuePairModel';
import { MakeService } from './../../Services/make.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FeatureService } from 'src/app/Services/feature.service';
import { VehicleService } from 'src/app/Services/vehicle.service';
import { VehicleModel } from 'src/app/Models/VehicleModel';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css'],
})
export class VehicleFormComponent implements OnInit {
  id: number = 0;
  makeid: number = 0;
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
    private route: ActivatedRoute,
    private vehicleService: VehicleService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.model = new VehicleModel();
    this.loadData();
    this.route.params.subscribe((p) => {
      if (p['id']) {
        this.id = p['id'];
        this.getDataForUpdate();
      }
    });

    this.dropdownSettings = {
      singleSelection: false,
      enableCheckAll: false,
      idField: 'id',
      textField: 'name',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true,
    };
    this.model = new VehicleModel();
    this.buildform();

    // this.loadData();
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
    });
  }

  save() {
    this.submitted = true;

    console.log(this.model.features);
    if (this.dataForm.invalid) {
      return;
    }

    console.log(this.model);

    if (this.id == 0) {
      this.vehicleService.post(this.model).subscribe({
        next: () => {
          this.router.navigate(['vehicles']);
        },
        error: (d) => {
          this.ErrorMessage(d.ErrorMessage, this.model.name);
        },
        complete: () => {
          this.showSuccess('Add Successfully', this.model.name);
        },
      });
    } else {
      this.vehicleService.put(this.model).subscribe({
        next: () => {},
        error: (d) => {
          this.ErrorMessage(d, this.model.name);
        },
        complete: () => {
          this.showSuccess('Edit Successfully', this.model.name);
        },
      });
    }
  }

  resetForm() {
    this.submitted = false;
    this.dataForm.reset();
    this.model = new VehicleModel();
  }

  onModelChange(e: any) {
    console.log(e);
    var selectedMake = this.makes.find((m) => m.id == e);
    this.models = selectedMake ? selectedMake.models : [];
  }

  onItemSelect(item: any) {
    this.featureIds.push(parseInt(item.id));
    console.log(this.featureIds);
    this.model.features = [...this.featureIds];
  }

  onItemDeSelect(item: any) {
    const index = this.featureIds.indexOf(item.id);
    if (index > -1) this.featureIds.splice(index, 1);
    this.model.features = [...this.featureIds];
  }
  // onSelectAll(items: any[]) {
  //   this.featureIds = items.map((p) => p.id);
  //   this.model.features = [...this.featureIds];
  // }

  onUnSelectAll() {
    this.featureIds = [];
    this.model.features = [];
  }

  getDataForUpdate() {
    this.vehicleService.getById(this.id).subscribe({
      next: (data) => {
        console.log(data);
        this.model.id = data.id;
        this.model.name = data.name;
        this.model.modelId = data.model.id;
        this.makeId = data.make.id;
        this.model.isRegistered = data.isRegistered;
        this.model.features = data.features.map((i: any) => i.id);
        this.featureIds = [...this.model.features];
        this.model.contact.name = data.contact.name;
        this.model.contact.email = data.contact.email;
        this.model.contact.phone = data.contact.phone;
        console.log(this.model.features);
      },
      error: (d) => {
        this.ErrorMessage(d.ErrorMessage, '');
      },
      complete: () => {
        this.onModelChange(this.makeId);
      },
    });
  }

  showSuccess(message: string, subject: string) {
    this.toastr.success(message, subject);
  }

  ErrorMessage(message: string, subject: string) {
    this.toastr.error(message, subject);
  }
}
