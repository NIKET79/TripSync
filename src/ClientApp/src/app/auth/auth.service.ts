import {Injectable} from '@angular/core';
import {environment} from "../../environments/environment.development";
import {RegisterModel} from "./models/RegisterModel";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Result} from "../models/Result";
import {Router} from "@angular/router";
import {CookieService} from "ngx-cookie-service";
import {LoginModel} from "./models/LoginModel";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl: string = environment.apiUrl+"/auth";
  constructor(private http: HttpClient,
              private router: Router,
              private cookie: CookieService,

              ) { }

  register(model: RegisterModel): Observable<Result> {
    const lang = this.cookie.get('lang') ?? 'en';
    return this.http.post<Result>(`${this.baseUrl}/register`, model, {
      headers: {
        'Content-Type':'application/json',
        'Localize-Language': lang
      }
    })
  }

  login(model: LoginModel): Observable<Result> {
    const lang = this.cookie.get('lang') ?? 'en';
    return this.http.post<Result>(`${this.baseUrl}/login`, model, {
      headers: {
        'Content-Type':'application/json',
        'Localize-Language': lang
      }
    })
  }

  isAuthenticated(): boolean {
    return true;
  }
}
