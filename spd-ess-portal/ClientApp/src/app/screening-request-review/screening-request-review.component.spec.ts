import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ScreeningRequestReviewComponent } from './screening-request-review.component';

describe('ScreeningRequestReviewComponent', () => {
  let component: ScreeningRequestReviewComponent;
  let fixture: ComponentFixture<ScreeningRequestReviewComponent>;

  beforeEach(async(() => {
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
