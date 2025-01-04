


import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
// import {BorrowFormComponent} from './borrow-form/borrow-form.component'
import { SignUpFormComponent } from './sign-up-form/sign-up-form.component'
import { MenuComponent } from './menu/menu.component';
import { SignInComponent } from './sign-in/sign-in.component'
import { OffersFormComponent } from './offersComponents/offers-form/offers-form.component';
import { RequestsComponent } from './requestsComponents/requests/requests.component';
import { PrivateAreaComponent } from './private-area/private-area.component';
import { BrowserModule } from '@angular/platform-browser'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
//import { ApdateofferComponent } from './apdate-offer/apdate-offer.component';
//import { LoginService } from './services/login.service';
// import { offersComponent } from './offers/offers.component';
import { MatButtonModule } from '@angular/material/button';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatToolbarModule } from '@angular/material/toolbar';
import { UpdateOfferComponent } from './offersComponents/update-offer/update-offer.component';
import { UpdateRequestComponent } from './requestsComponents/update-request/update-request.component';
import { IgnoreRequestComponent } from './requestsComponents/ignore-request/ignore-request.component';
import { AcceptRequestComponent } from './requestsComponents/accept-request/accept-request.component';
import { AboutComponent } from './about/about.component';
import { ViewActiveRequestsComponent } from './requestsComponents/view-active-requests/view-active-requests.component';
import { ViewHistoryRequestsComponent } from './requestsComponents/view-history-requests/view-history-requests.component';
import { ViewHistoryOffersComponent } from './offersComponents/view-history-offers/view-history-offers.component';
import { ViewActiveOffersComponent } from './offersComponents/view-active-offers/view-active-offers.component';
import { ViewOwnOffersComponent } from './offersComponents/view-own-offers/view-own-offers.component';
import { ViewOwnRequestsComponent } from './requestsComponents/view-own-requests/view-own-requests.component';
import { CommonModule } from '@angular/common';
import { UpdatePersonComponent } from './update-person/update-person.component';
import { AllRequestsComponent } from './requestsComponents/all-requests/all-requests.component';
import { AllOffersComponent } from './offersComponents/all-offers/all-offers.component';
import { SetPersonStatusComponent } from './set-person-status/set-person-status.component';


const routes: Routes = [
  {
    path: '',
    component: AboutComponent,
    pathMatch: 'full'
  },
  { path: 'about', component: AboutComponent },
  { path: 'signInForm', component: SignInComponent },
  { path: 'signUpForm', component: SignUpFormComponent },
  { path: 'updateDetails', component: UpdatePersonComponent },
  
  {
    path: 'privateArea', component: PrivateAreaComponent,
    children: [
      {
        path: 'offersForm', component: OffersFormComponent//,pathMatch: "full"
      },
      { path: 'AllOffers', component: AllOffersComponent },
      { path: 'AllRequests', component: AllRequestsComponent },
  
      { path: 'RequestsForm', component: RequestsComponent },
      { path: 'RequestsHistory', component: ViewHistoryRequestsComponent },
      { path: 'ActiveRequests', component: ViewActiveRequestsComponent },
      { path: 'ActiveRequests/:id', component: ViewActiveRequestsComponent },
      { path: 'OwnRequests', component: ViewOwnRequestsComponent },
      { path: 'OffersHistory', component: ViewHistoryOffersComponent },
      { path: 'ActiveOffers', component: ViewActiveOffersComponent },
      { path: 'OwnOffers', component: ViewOwnOffersComponent }
      , { path: 'updateOffer/:id', component: UpdateOfferComponent }
      , { path: 'updateRequest/:id', component: UpdateRequestComponent }
      , { path: 'personStatus', component: SetPersonStatusComponent }
    ]
  }
  , { path: 'ignore-request/:offerId/:reqId', component: IgnoreRequestComponent }
  , { path: 'accept-request/:offerId/:reqId', component: AcceptRequestComponent }
]



@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    SignUpFormComponent,
    SignInComponent,
    OffersFormComponent,
    RequestsComponent,
    PrivateAreaComponent,
    UpdateOfferComponent,
    UpdateRequestComponent,
    IgnoreRequestComponent,
    AcceptRequestComponent,
    AboutComponent,
    ViewActiveRequestsComponent,
    ViewHistoryRequestsComponent,
    ViewHistoryOffersComponent,
    ViewActiveOffersComponent,
    ViewOwnRequestsComponent,
    ViewOwnOffersComponent,
    UpdatePersonComponent,
    AllRequestsComponent,
    AllOffersComponent,
    SetPersonStatusComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forRoot(routes),
    HttpClientModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatTableModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
    MatSelectModule,
    MatRadioModule,
    MatToolbarModule

  ],
  exports: [

    // MatTableModule,
    // MatSortModule,
    // MatFormFieldModule,
    // MatInputModule,
    // MatDatepickerModule,
    // MatNativeDateModule,
    // MatIconModule,
    // MatSelectModule,
    // MatRadioModule,
    // MatButtonModule
  ],

  providers: [],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
