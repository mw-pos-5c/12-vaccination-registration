import {Injectable} from '@angular/core';
import TimeSlot from "../models/TimeSlot";
import Patient from "../models/Patient";
import {HttpClient} from "@angular/common/http";
import VaccConfirm from "../models/VaccConfirm";

@Injectable({
  providedIn: 'root'
})
export class VaccRegService {

  private url = 'http://localhost:5000/VaccReg/';

  step: number = 0;

  ssn: number = -1;
  pin: number = -1;
  date: string = '';

  confirmation: VaccConfirm | null = null;
  patient: Patient | null = null;
  timeSlots: TimeSlot[] = [];


  constructor(private http: HttpClient) {
  }


  submitSsn(ssn: number, pin: number): Promise<boolean> {
    return new Promise<boolean>(resolve => {
      this.http.get<Patient>(this.url + 'CheckSsn?ssn=' + ssn + '&pin=' + pin).subscribe(value => {
        if (value) {
          this.patient = value;
          this.ssn = ssn;
          this.pin = pin;
          this.step = 1;
          resolve(true);
        }
        resolve(false);
      });
    });
  }

  submitDate(date: string): Promise<boolean> {
    return new Promise<boolean>((resolve) => {
      this.http.get<TimeSlot[]>(this.url + 'GetTimeSlots?day=' + date).subscribe(value => {
        this.timeSlots = value;
        if (this.timeSlots.length > 0) {
          this.step = 2;
          this.date = date;
          resolve(true);
        }
        resolve(false);
      });
    });
  }

  submitSlot(slotId: number): Promise<boolean> {
    return new Promise<boolean>(resolve => {
      this.http.get<VaccConfirm>(
        this.url + 'RegisterTimeSlot?ssn=' + this.ssn +
        '&pin=' + this.pin +
        '&day=' + this.date +
        '&slotId=' + slotId)
        .subscribe(value => {
        if (value) {
          this.confirmation = value
          this.step = 3;
          resolve(true);
        }
          resolve(false);
      })
    })
  }


}
