import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IndexpageComponent } from './indexpage/indexpage.component';
import { LoginpageComponent } from './loginpage/loginpage.component';
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


@NgModule({
  declarations: [
    AppComponent,
    HeaderLoggedInComponent,
    HeaderLoggedOutComponent,
    FooterComponent,
    IndexpageComponent,
    LoginpageComponent,
    ProfilepageComponent,
    PostDetailsComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    RouterModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatSidenavModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
    OrderModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
