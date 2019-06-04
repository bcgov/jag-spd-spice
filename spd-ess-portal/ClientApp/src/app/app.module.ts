import { NgModule } from '@angular/core';
import { CdkTableModule } from '@angular/cdk/table';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule, Title } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule, NgbDropdown } from '@ng-bootstrap/ng-bootstrap';
import { StoreModule } from '@ngrx/store';
import { NgBusyModule } from 'ng-busy';
import { ChartsModule } from 'ng2-charts';
import { BsDatepickerModule, AlertModule } from 'ngx-bootstrap';
import { CookieService } from 'ngx-cookie-service';
import { FileDropModule } from 'ngx-file-drop';
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

import { metaReducers, reducers } from './app-state/reducers/reducers';

import { ScreeningRequestDataService } from './services/screening-request-data.service';
import { UserDataService } from './services/user-data.service';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ScreeningRequestFormComponent } from './screening-request-form/screening-request-form.component';
import { ScreeningRequestReviewComponent } from './screening-request-review/screening-request-review.component';
import { ScreeningRequestConfirmationComponent } from './screening-request-confirmation/screening-request-confirmation.component';

import { FieldComponent } from './shared/field/field.component';
import { FileUploaderComponent } from './shared/file-uploader/file-uploader.component';

@NgModule({
  declarations: [
    AppComponent,
    FieldComponent,
    FileUploaderComponent,
    NotFoundComponent,
    ScreeningRequestFormComponent,
    ScreeningRequestReviewComponent,
    ScreeningRequestConfirmationComponent,
  ],
  imports: [
    AlertModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    BrowserModule,
    BsDatepickerModule,
    ChartsModule,
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
    NgbModule,
    ReactiveFormsModule,
    StoreModule.forRoot(reducers, { metaReducers }),
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
    CookieService,
    NgbDropdown,
    ScreeningRequestDataService,
    Title,
    UserDataService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
