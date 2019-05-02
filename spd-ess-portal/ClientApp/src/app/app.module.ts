import { BrowserModule, Title } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule, NgbDropdown } from '@ng-bootstrap/ng-bootstrap';
import { CookieService } from 'ngx-cookie-service';
import { AppRoutingModule } from './app-routing.module';
import { ChartsModule } from 'ng2-charts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { 
  MatAutocompleteModule,
  MatButtonModule,
  MatButtonToggleModule,
  MatCardModule,
  MatCheckboxModule,
  MatChipsModule,
  MatDatepickerModule,
  MatDialogModule,
  MatDividerModule,
  MatExpansionModule,
  MatGridListModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatMenuModule,
  MatNativeDateModule,
  MatPaginatorModule,
  MatProgressBarModule,
  MatProgressSpinnerModule,
  MatRadioModule,
  MatRippleModule,
  MatSelectModule,
  MatSidenavModule,
  MatSliderModule,
  MatSlideToggleModule,
  MatSnackBarModule,
  MatSortModule,
  MatStepperModule,
  MatTableModule,
  MatTabsModule,
  MatToolbarModule,
  MatTooltipModule
} from '@angular/material';
import { CdkTableModule } from '@angular/cdk/table';

import { AccountDataService } from './services/account-data.service';
import { ContactDataService } from './services/contact-data.service';
import { ApplicationDataService } from './services/application-data.service';
import { LegalEntityDataService } from './services/legal-entity-data.service';
import { LicenseDataService } from './services/license-data.service';
import { PaymentDataService } from './services/payment-data.service';
import { AppComponent } from './app.component';
import { BceidConfirmationComponent } from './bceid-confirmation/bceid-confirmation.component';
import { SearchBoxDirective } from './search-box/search-box.directive';
import { GeneralDataService } from './general-data.service';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { DynamicsDataService } from './services/dynamics-data.service';
import { StaticComponent } from './static/static.component';
import { HomeComponent } from './home/home.component';
import { PolicyDocumentDataService } from './services/policy-document-data.service';

import { StatusBadgeComponent } from './status-badge/status-badge.component';
import { SurveyDataService } from './services/survey-data.service';
import { VoteDataService } from './services/vote-data.service';
import { NewsletterDataService } from './services/newsletter-data.service';
import { UserDataService } from './services/user-data.service';
import { NotFoundComponent } from './not-found/not-found.component';
import { FileDropModule } from 'ngx-file-drop';
import { FileUploaderComponent } from './shared/file-uploader/file-uploader.component';

import { NgBusyModule } from 'ng-busy';

import { BsDatepickerModule, AlertModule } from 'ngx-bootstrap';
import { TiedHouseConnectionsDataService } from './services/tied-house-connections-data.service';
import { CanDeactivateGuard } from './services/can-deactivate-guard.service';
import { BCeidAuthGuard } from './services/bceid-auth-guard.service';
import { ServiceCardAuthGuard } from './services/service-card-auth-guard.service';
import { metaReducers, reducers } from './app-state/reducers/reducers';
import { StoreModule } from '@ngrx/store';
import { TermsAndConditionsComponent } from './lite/terms-and-conditions/terms-and-conditions.component';
import { AliasDataService } from './services/alias-data.service';
import { PreviousAddressDataService } from './services/previous-address-data.service';
import { WorkerDataService } from './services/worker-data.service.';
import { FieldComponent } from './shared/field/field.component';

@NgModule({
  declarations: [
    AppComponent,
    BceidConfirmationComponent,
    BreadcrumbComponent,   
    FieldComponent,
    FileUploaderComponent,    
    HomeComponent,
    NotFoundComponent,    
    SearchBoxDirective,
    StaticComponent,
    StatusBadgeComponent,
    TermsAndConditionsComponent,
  ],
  imports: [
    ChartsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    BrowserModule,
    CdkTableModule,
    FileDropModule,
    FormsModule,
    HttpClientModule,
    MatAutocompleteModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSlideToggleModule,
    MatSliderModule,
    MatSnackBarModule,
    MatSortModule,
    MatStepperModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    NgBusyModule,
    NgbModule.forRoot(),
    ReactiveFormsModule,
    BsDatepickerModule.forRoot(),
    StoreModule.forRoot(reducers, { metaReducers }),
    AlertModule.forRoot()
  ],
  exports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    BrowserModule,
    CdkTableModule,
    FileDropModule,
    FormsModule,
    HttpClientModule,
    MatAutocompleteModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSlideToggleModule,
    MatSliderModule,
    MatSnackBarModule,
    MatSortModule,
    MatStepperModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    NgbModule,
    ReactiveFormsModule,
  ],
  providers: [
    AccountDataService,
    ApplicationDataService,
    LegalEntityDataService,
    LicenseDataService,
    AliasDataService,
    BCeidAuthGuard,
    CanDeactivateGuard,
    ContactDataService,
    CookieService,
    DynamicsDataService,
    GeneralDataService,
    NewsletterDataService,
    NgbDropdown,
    PaymentDataService,
    PolicyDocumentDataService,
    PreviousAddressDataService,
    ServiceCardAuthGuard,
    SurveyDataService,
    TiedHouseConnectionsDataService,
    Title,
    UserDataService,
    VoteDataService,
    WorkerDataService,
  ],  
  bootstrap: [AppComponent]
})
export class AppModule { }
