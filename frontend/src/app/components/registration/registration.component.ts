import { Component, OnInit } from '@angular/core';
import {VaccRegService} from "../../services/vacc-reg.service";
import {FormControl, FormGroup} from "@angular/forms";

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

  constructor(public service: VaccRegService) { }

  ngOnInit(): void {
    this.ssnFormGroup = new FormGroup({
      ssn: new FormControl(''),
      pin: new FormControl('')
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
