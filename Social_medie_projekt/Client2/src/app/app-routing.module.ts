import { Component, inject, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './_helpers/auth.guard';

import { IndexpageComponent } from './pages/indexpage/indexpage.component';
import { PostDetailsComponent } from './pages/post-details/post-detail.component';
import { LoginpageComponent } from './pages/loginpage/loginpage.component';
import { CreatePostPageComponent } from './pages/create-postpage/create-postpage.component';
import { EditPostComponent } from './pages/edit-post/edit-post.component';
import { ProfilepageComponent } from './pages/profilepage/profilepage.component';
import { OtherUserProfilePageComponent } from './pages/profilepage-otheruser/profilepage.component';

const routes: Routes = [
  {
    path: '',
    component: LoginpageComponent,
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
    component: OtherUserProfilePageComponent,
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
