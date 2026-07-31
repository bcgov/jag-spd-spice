import { Component, Input } from "@angular/core";

@Component({
  selector: "app-site-error",
  templateUrl: "./site-error.component.html",
  styleUrls: ["./site-error.component.scss"],
})
export class SiteErrorComponent {
  @Input() error?: Error;

  get isVisible(): boolean {
    return !!this.error;
  }
}
