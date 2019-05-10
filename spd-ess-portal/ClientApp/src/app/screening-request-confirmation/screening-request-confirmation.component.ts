import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-screening-request-confirmation',
  templateUrl: './screening-request-confirmation.component.html'
})
export class ScreeningRequestConfirmationComponent {
  constructor(private router: Router,
  ) { }
  
  gotoForm() {
    this.router.navigate(['/']);
  }
}
