import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent } from './footer/footer.component';
import { NavbarComponent } from "./navbar/navbar.component";
import { RouterLink } from "@angular/router";
import { MainPageComponent } from './main-page/main-page.component';
import { ToastrModule } from "ngx-toastr";
import { NotificationComponent } from './notification/notification.component';


@NgModule({
  declarations: [
    FooterComponent,
    NavbarComponent,
    MainPageComponent,
    NotificationComponent,
  ],
  exports: [
    FooterComponent,
    NavbarComponent,
    MainPageComponent,
    NotificationComponent
  ],
  imports: [
    CommonModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      preventDuplicates: true,
      maxOpened: 3,
      positionClass: 'toast-top-right',
      toastComponent: NotificationComponent,
      toastClass: ""
    }),
    RouterLink,
  ]
})
export class AppCommonModule { }
