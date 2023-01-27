import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IndexpageComponent } from './indexpage/indexpage.component';
import { LoginpageComponent } from './loginpage/loginpage.component';
import { CreatepageComponent } from './loginpage/createUserpage.component';
import { ProfilepageComponent } from './profilepage/profilepage.component';
import { HeaderLoggedInComponent } from './headers/headerLoggedIn.component';
import { HeaderLoggedOutComponent } from './headers/headerLoggedOut.component';
import { FooterComponent } from './footer.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppComponent,
    HeaderLoggedInComponent,
    HeaderLoggedOutComponent,
    FooterComponent,
    IndexpageComponent,
    LoginpageComponent,
    ProfilepageComponent,
    CreatepageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
