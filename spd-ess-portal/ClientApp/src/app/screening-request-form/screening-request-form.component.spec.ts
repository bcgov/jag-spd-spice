import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ScreeningRequestFormComponent } from './screening-request-form.component';

describe('ScreeningRequestFormComponent', () => {
  let component: ScreeningRequestFormComponent;
  let fixture: ComponentFixture<ScreeningRequestFormComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ScreeningRequestFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScreeningRequestFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
