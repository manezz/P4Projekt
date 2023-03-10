import { inject, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './_helpers/auth.guard';
import { Role } from './_models/role';

import { IndexpageComponent } from './indexpage/indexpage.component';
import { PostDetailsComponent } from './post-details/post-detail.component';
import { LoginpageComponent } from './loginpage/loginpage.component';
import { CreatePostPageComponent } from './create-postpage/create-postpage.component';
import { EditPostComponent } from './edit-post/edit-post.component';

//canActivate: [() => inject(myGuard).canActivate()]

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
    //canActivate: [() => inject(AuthGuard).canActivate],
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
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
