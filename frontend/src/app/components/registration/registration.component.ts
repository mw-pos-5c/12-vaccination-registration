import { Component, OnInit } from '@angular/core';
import {VaccRegService} from "../../services/vacc-reg.service";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  ssnFormGroup!: FormGroup;
  dateFormGroup!: FormGroup;
  slotFormGroup!: FormGroup;

  ssnError: boolean = false;
  dateError: boolean = false;

  constructor(public service: VaccRegService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.ssnFormGroup = this.fb.group({
      ssn: ['', [Validators.required, Validators.max(9999999999), Validators.min(1000000000)]],
      pin: ['', [Validators.required, Validators.max(999999), Validators.min(100000)]]
    });

    this.dateFormGroup = new FormGroup({
      date: new FormControl('')
    });

    this.slotFormGroup = new FormGroup({
      slot: new FormControl('')
    });
  }

  ssnFormSubmit(): void {
    this.service.submitSsn(this.ssnFormGroup.value.ssn, this.ssnFormGroup.value.pin).then(value => {
      if (!value) {
        this.ssnError = true;
      }
    })
  }

  dateFormSubmit(): void {
    this.service.submitDate(this.dateFormGroup.value.date).then(value => {
      if (!value) {
        this.dateError = true;
      }
    })
  }

  slotFormSubmit(): void {
    this.service.submitSlot(this.slotFormGroup.value.slot).then(value => {
      alert("Could not create TimeSlot! Idk Why :(")
    })
  }

}
