import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {authRoutes} from "./auth/auth.module";
import { MainPageComponent } from "./AppCommon/main-page/main-page.component";

const routes: Routes = [
  { path: '', component: MainPageComponent, title: 'TripSync' },
  { path: 'auth', children: authRoutes},
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
