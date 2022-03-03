import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ScreeningRequestConfirmationComponent } from './screening-request-confirmation.component';

describe('ScreeningRequestConfirmationComponent', () => {
  let component: ScreeningRequestConfirmationComponent;
  let fixture: ComponentFixture<ScreeningRequestConfirmationComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ScreeningRequestConfirmationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScreeningRequestConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
