import { Component } from '@angular/core';
import {CommonService} from "../common.service";

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent {
  constructor(private common: CommonService) {
  }

  setLang(lang: string) {
    this.common.setLang(lang);
  }

  isDarkTheme() {
    return this.common.currentTheme == 'dark'
  }
}
