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
import { CreateUserPageComponent } from './loginpage/createUserpage.component';
import { CreatePostPageComponent } from './profilepage/createPostpage.component';
import { ProfilepageComponent } from './profilepage/profilepage.component';
import { HeaderLoggedInComponent } from './header-footer/headerLoggedIn.component';
import { HeaderLoggedOutComponent } from './header-footer/headerLoggedOut.component';
import { FooterComponent } from './header-footer/footer.component';
import { PostDetailsComponent } from './post-details/post-details.component';
import { EditPostComponent } from './post-details/editPost.component';

//for Mat popups
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialogModule } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule} from '@angular/material/sidenav';
import { MatIconModule} from '@angular/material/icon';
import { MatDividerModule} from '@angular/material/divider';


@NgModule({
  declarations: [
    AppComponent,
    IndexpageComponent,
    ChatComponent,
    LoginpageComponent,
    CreateUserPageComponent,
    CreatePostPageComponent,
    ProfilepageComponent,
    HeaderLoggedInComponent,
    HeaderLoggedOutComponent,
    FooterComponent,
    PostDetailsComponent,
    EditPostComponent,

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

    MatIconModule,
    MatDividerModule,
    MatButtonModule, 
    MatDialogModule, 
    MatFormFieldModule,
    MatInputModule, 
    BrowserAnimationsModule,

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { 

}
