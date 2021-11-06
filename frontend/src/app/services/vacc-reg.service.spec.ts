import { TestBed } from '@angular/core/testing';

import { VaccRegService } from './vacc-reg.service';

describe('VaccRegService', () => {
  let service: VaccRegService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VaccRegService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
