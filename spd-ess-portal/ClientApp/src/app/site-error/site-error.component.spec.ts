import { ComponentFixture, TestBed, waitForAsync } from "@angular/core/testing";
import { SiteErrorComponent } from "./site-error.component";

describe("SiteErrorComponent", () => {
  let component: SiteErrorComponent;
  let fixture: ComponentFixture<SiteErrorComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [SiteErrorComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteErrorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
