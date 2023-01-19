import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './_helpers/auth.guard';
import { Role } from './_models/role'

import { IndexpageComponent } from './indexpage/indexpage.component';
import { LoginpageComponent } from './loginpage/loginpage.component';
import { ProfilepageComponent } from './profilepage/profilepage.component';
import { PostPageComponent } from './postpage/postpage.component';

const routes: Routes = [
    //canActivate gør at man skal have en user eller admin rolle før man kan tilgå pathen 
  { path: '', component: LoginpageComponent },
  { path: 'post', component: PostPageComponent, canActivate: [AuthGuard], data: {roles: [Role.User] && [Role.Admin] }},
  { path: 'main', component: IndexpageComponent, canActivate: [AuthGuard], data: {roles: [Role.User] && [Role.Admin] } }, 
  { path: 'profile', component: ProfilepageComponent, canActivate: [AuthGuard], data: {roles: [Role.User] && [Role.Admin] } },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
