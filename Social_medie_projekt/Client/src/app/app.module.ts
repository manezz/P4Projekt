import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// Page components
import { AppComponent } from './app.component';
import { IndexpageComponent } from './indexpage/indexpage.component';
import { LoginpageComponent } from './loginpage/loginpage.component';
import { CreatepageComponent } from './loginpage/createUserpage.component';
import { ProfilepageComponent } from './profilepage/profilepage.component';
import { HeaderLoggedInComponent } from './headers/headerLoggedIn.component';
import { HeaderLoggedOutComponent } from './headers/headerLoggedOut.component';
import { FooterComponent } from './footer.component';

//for Mat popups
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialogModule } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

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
    BrowserAnimationsModule,
    MatDialogModule, 
    MatInputModule, 
    MatButtonModule, 
    MatCardModule, 
    MatFormFieldModule

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
