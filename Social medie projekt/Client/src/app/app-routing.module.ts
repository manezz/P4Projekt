import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexpageComponent } from './indexpage/indexpage.component';
import { LoginpageComponent } from './loginpage/loginpage.component';
import { ProfilepageComponent } from './profilepage/profilepage.component';

const routes: Routes = [
  // { path: "", loadComponent: () => import('./indexpage/indexpage.component').then(it => it.IndexpageComponent)}
  { path: '', component: LoginpageComponent },
  { path: 'main', component: IndexpageComponent },
  { path: 'profile/', component: ProfilepageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
