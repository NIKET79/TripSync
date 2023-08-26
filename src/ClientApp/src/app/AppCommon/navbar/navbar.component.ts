import { Component } from '@angular/core';
import {CommonService} from "../common.service";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  constructor(private common: CommonService) {
  }

  toggleTheme() {
    const theme = this.common.currentTheme;
    this.common.setTheme(theme == 'dark' ? 'light' : 'dark');
  }


  isDarkTheme() {
    return this.common.currentTheme == 'dark'
  }
}
