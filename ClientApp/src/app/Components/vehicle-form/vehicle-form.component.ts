import { KeyValuePair } from './../../Models/KeyValuePairModel';
import { MakeSerice } from './../../Services/make.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

  makes: KeyValuePair[];
  vehicle : any;
  dataForm: FormGroup;

  get frm() { return this.dataForm.controls; }
  get fg() { return this.dataForm; }
  constructor(

    private makeService: MakeSerice
  ) { }

  ngOnInit(): void {
    this.buildform();
    this.loadData();
  }

  buildform() {
    this.dataForm = new FormGroup({
      model: new FormControl('', Validators.compose([Validators.required])),
      make: new FormControl('', Validators.compose([Validators.required]))
    })
  }

  loadData() {
    this.makeService.getMakes()
      .subscribe(data =>{
        this.makes = data
        console.log(data)
      }
      );
  }

  
  onModelChange(e : any) {
     debugger;
     console.log(e);
  }


}
