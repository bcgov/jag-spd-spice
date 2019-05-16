import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ScreeningRequestFormComponent } from './screening-request-form/screening-request-form.component';
import { ScreeningRequestReviewComponent } from './screening-request-review/screening-request-review.component';
import { ScreeningRequestConfirmationComponent } from './screening-request-confirmation/screening-request-confirmation.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { CanDeactivateGuard } from './services/can-deactivate-guard.service';
import { BCeidAuthGuard } from './services/bceid-auth-guard.service';
import { ServiceCardAuthGuard } from './services/service-card-auth-guard.service';


const routes: Routes = [
  {
    path: '',
    component: ScreeningRequestFormComponent
  },
  {
    path: 'review-submission',
    component: ScreeningRequestReviewComponent
  },
  {
    path: 'request-submitted',
    component: ScreeningRequestConfirmationComponent
  },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {scrollPositionRestoration: 'enabled'})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
