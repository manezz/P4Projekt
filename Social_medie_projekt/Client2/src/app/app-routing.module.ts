import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { IndexpageComponent } from './indexpage/indexpage.component';

const routes: Routes = [{ path: 'main', component: IndexpageComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
