import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthStepComponent } from './auth-step.component';

describe('AuthStepComponent', () => {
  let component: AuthStepComponent;
  let fixture: ComponentFixture<AuthStepComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AuthStepComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthStepComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
