import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './_helpers/auth.guard';

const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./pages/loginpage/loginpage.component').then(
        (x) => x.LoginpageComponent
      ),
  },
  {
    path: 'createuser',
    loadComponent: () =>
      import('./pages/create-userpage/create-userpage.component').then(
        (x) => x.CreateUserPageComponent
      ),
  },
  {
    path: 'createpost',
    loadComponent: () =>
      import('./pages/create-postpage/create-postpage.component').then(
        (x) => x.CreatePostPageComponent
      ),
  },
  {
    path: 'main',
    loadComponent: () =>
      import('./pages/indexpage/indexpage.component').then(
        (x) => x.IndexpageComponent
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'post-details/:postId',
    loadComponent: () =>
      import('./pages/post-details/post-detail.component').then(
        (x) => x.PostDetailsComponent
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'editPost/:postId',
    loadComponent: () =>
      import('./pages/edit-post/edit-post.component').then(
        (x) => x.EditPostComponent
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'profile',
    loadComponent: () =>
      import('./pages/profilepage/profilepage.component').then(
        (x) => x.ProfilepageComponent
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'profile/:userId',
    loadComponent: () =>
      import('./pages/profilepage/profilepage.component').then(
        (x) => x.ProfilepageComponent
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'profile-update',
    loadComponent: () =>
      import('./pages/update-userpage/update-userpage.component').then(
        (x) => x.UpdateUserPageComponent
      ),
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
