import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ScreeningRequestReviewComponent } from './screening-request-review.component';

describe('ScreeningRequestReviewComponent', () => {
  let component: ScreeningRequestReviewComponent;
  let fixture: ComponentFixture<ScreeningRequestReviewComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ScreeningRequestReviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScreeningRequestReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
