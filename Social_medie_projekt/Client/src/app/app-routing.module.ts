import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './_helpers/auth.guard';
import { Role } from './_models/role'

import { IndexpageComponent } from './indexpage/indexpage.component';
import { LoginpageComponent } from './loginpage/loginpage.component';
import { CreateUserPageComponent } from './loginpage/createUserpage.component';
import { CreatePostPageComponent } from './profilepage/createPostpage.component';
import { ProfilepageComponent } from './profilepage/profilepage.component';
import { OtherUserProfilePageComponent } from './profilepage/otherUserProfilePage.component';
import { PostDetailsComponent } from './post-details/post-details.component';
import { EditPostComponent } from './post-details/editPost.component';
import { ChatComponent } from './indexpage/chat.component';

const routes: Routes = [
    //canActivate gør at man skal have en user eller admin rolle før man kan tilgå pathen 
  { path: '', component: LoginpageComponent },
  { path: 'createuser', component: CreateUserPageComponent },
  { path: 'createpost', component: CreatePostPageComponent, canActivate: [AuthGuard] },
  { path: 'main', component: IndexpageComponent, canActivate: [AuthGuard] }, 
  { path: 'profile', component: ProfilepageComponent, canActivate: [AuthGuard] },
  { path: 'profile/:userId', component: OtherUserProfilePageComponent, canActivate: [AuthGuard] }, // ":userId" as the params value
  { path: 'post-details/:postId', component: PostDetailsComponent, canActivate: [AuthGuard] },
  { path: 'editPost/:postId', component: EditPostComponent, canActivate: [AuthGuard] },
  { path: 'chat', component: ChatComponent, canActivate: [AuthGuard] },
]


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
