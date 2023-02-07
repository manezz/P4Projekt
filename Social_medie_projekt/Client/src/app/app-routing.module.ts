import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './_helpers/auth.guard';
import { Role } from './_models/role'

import { IndexpageComponent } from './indexpage/indexpage.component';
import { LoginpageComponent } from './loginpage/loginpage.component';
import { CreatepageComponent } from './loginpage/createUserpage.component';
import { ProfilepageComponent } from './profilepage/profilepage.component';
import { ChatComponent } from './indexpage/chat.component';

const routes: Routes = [
    //canActivate gør at man skal have en user eller admin rolle før man kan tilgå pathen 
  { path: '', component: LoginpageComponent },
  { path: 'createuser', component: CreatepageComponent },
  { path: 'main', component: IndexpageComponent, canActivate: [AuthGuard] }, 
  { path: 'profile', component: ProfilepageComponent, canActivate: [AuthGuard] },
  { path: 'profile/:userId', component: ProfilepageComponent, canActivate: [AuthGuard] },
  { path: 'chat', component: ChatComponent },
  // { path: 'admin', component: AdminpageComponent, canActivate: [AuthGuard], data: {roles: [Role.Admin] } },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
