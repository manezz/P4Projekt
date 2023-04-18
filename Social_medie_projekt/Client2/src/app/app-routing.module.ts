import { Component, inject, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './_helpers/auth.guard';

import { IndexpageComponent } from './pages/indexpage/indexpage.component';
import { PostDetailsComponent } from './pages/post-details/post-detail.component';
import { LoginpageComponent } from './pages/loginpage/loginpage.component';
import { CreatePostPageComponent } from './pages/create-postpage/create-postpage.component';
import { CreateUserPageComponent } from './pages/create-userpage/create-userpage.component';
import { EditPostComponent } from './pages/edit-post/edit-post.component';
import { ProfilepageComponent } from './pages/profilepage/profilepage.component';
import { OtherUserProfilePageComponent } from './pages/profilepage-otheruser/profilepage-otheruser.component';
import { UpdateUserPageComponent } from './pages/update-userpage/update-userpage.component';

const routes: Routes = [
  {
    path: '',
    component: LoginpageComponent,
  },
  {
    path: 'createuser',
    component: CreateUserPageComponent,
  },
  { path: 'createpost', component: CreatePostPageComponent },
  {
    path: 'main',
    component: IndexpageComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'post-details/:postId',
    component: PostDetailsComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'editPost/:postId',
    component: EditPostComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'profile',
    component: ProfilepageComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'profile/:userId',
    component: ProfilepageComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'profile-update',
    component: UpdateUserPageComponent,
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
