import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// Page components
import { AppComponent } from './app.component';
import { IndexpageComponent } from './indexpage/indexpage.component';
import { ChatComponent } from './indexpage/chat.component';
import { LoginpageComponent } from './loginpage/loginpage.component';
import { CreatepageComponent } from './loginpage/createUserpage.component';
import { ProfilepageComponent } from './profilepage/profilepage.component';
import { HeaderLoggedInComponent } from './headers/headerLoggedIn.component';
import { HeaderLoggedOutComponent } from './headers/headerLoggedOut.component';
import { FooterComponent } from './footer.component';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule} from '@angular/material/sidenav';
import { MatButtonModule} from '@angular/material/button';
import { MatIconModule} from '@angular/material/icon';
import { MatDividerModule} from '@angular/material/divider';
import { OrderModule } from 'ngx-order-pipe';
import { PostDetailsComponent } from './post-details/post-details.component';
import { HeaderLoggedInComponent } from './header-footer/headerLoggedIn.component';
import { HeaderLoggedOutComponent } from './header-footer/headerLoggedOut.component';
import { FooterComponent } from './header-footer/footer.component';

//for Mat popups
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
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
    ChatComponent,
    LoginpageComponent,
    ProfilepageComponent,
    CreatepageComponent
    PostDetailsComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatSidenavModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
    OrderModule
    BrowserAnimationsModule,

    MatButtonModule, 
    MatDialogModule, 
    MatFormFieldModule,
    MatInputModule, 

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { 

}
