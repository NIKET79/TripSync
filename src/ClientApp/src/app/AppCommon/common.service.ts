import {inject, Injectable, Injector} from '@angular/core';
import {BehaviorSubject} from "rxjs";
import {TranslatePipe, TranslateService} from "@ngx-translate/core";
import {HttpClient} from "@angular/common/http";
import {CookieService} from "ngx-cookie-service";
import {ToastrService} from "ngx-toastr";
import {NotificationComponent} from "./notification/notification.component";

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  private body: HTMLElement = document.body;
  public theme: BehaviorSubject<string> = new BehaviorSubject<string>('light');
  public get currentTheme() {
    return this.theme.value;
  }

  constructor(
    private translator: TranslateService,
    private cookie: CookieService,
    private toastr: ToastrService) {
    translator.addLangs(['en', 'uk', 'de'])
    console.log("absd")
  }

  public setTheme(theme: string) : void {
    this.theme.next(theme);
    this.body.classList.remove('dark', 'light');
    this.body.classList.add(theme);
  }

  public static ToPreviousPage() {
    window.history.back()
  }

  public setLang(lang: string) {
    if (this.translator.currentLang !== lang && this.translator.getLangs().includes(lang)) {
      this.translator.use(lang);
      this.cookie.set('lang', lang, { secure: true, sameSite: "Strict" })
    }
  }

  public setLangOnLoad() {
    const lang = this.cookie.get('lang');
    if (this.translator.getLangs().includes(lang)) {
      this.setLang(lang);
    } else {
      this.setLang('en')
    }
  }

  public showToast(level: 'success' |
    'error' | 'info' | 'warning', title: string, message: string) {
    const toastFunctions = {
      success: this.toastr.success,
      error: this.toastr.error,
      warning: this.toastr.warning,
      info: this.toastr.info
    };

    const toastFunction = toastFunctions[level];

    if (toastFunction) {
      toastFunction.call(this.toastr, message, title);
    }
  }
}
