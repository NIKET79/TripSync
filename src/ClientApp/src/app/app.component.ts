import {Component, OnInit} from '@angular/core';
import {CommonService} from "./AppCommon/common.service";
import {NavigationEnd, NavigationStart, Router} from "@angular/router";
import {combineLatest, filter, map, Observable, startWith} from "rxjs";
import {TranslateService} from "@ngx-translate/core";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'TripSync';
  private routesWithHiddenNavs: string[] = [
    '/auth/login',
    '/auth/register'
  ]

  hideNavbarFooter$: Observable<boolean>;

  constructor(private router: Router, private common: CommonService) {
    common.setLangOnLoad()

    const navigationEnd$ = this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    );

    this.hideNavbarFooter$ = combineLatest([
      navigationEnd$,
      navigationEnd$.pipe(startWith(null))
    ]).pipe(
      map(([event]) => {
        if (event instanceof NavigationEnd) {
          return this.routesWithHiddenNavs.some(route => event.urlAfterRedirects.includes(route));
        }
        return false;
      })
    );
  }
}
