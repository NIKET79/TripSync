import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { Route, RouterLink } from "@angular/router";
import { RegisterComponent } from './register/register.component';
import { ReactiveFormsModule } from "@angular/forms";

export const authRoutes: Route[] = [
  { path: 'login', component: LoginComponent, title: 'Login' },
  { path: 'register', component: RegisterComponent, title: 'Register' }
]
@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    CommonModule,
    RouterLink,
    ReactiveFormsModule,
  ]
})
export class AuthModule { }
