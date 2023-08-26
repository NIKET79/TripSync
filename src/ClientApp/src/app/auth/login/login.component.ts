import { Component } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {RxwebValidators} from "@rxweb/reactive-form-validators";
import {CommonService} from "../../AppCommon/common.service";
import {AuthService} from "../auth.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginGroup: FormGroup;
  triedToSubmit = false;

  submitErrors: string[] | undefined;

  constructor(fb: FormBuilder, private common: CommonService, private auth: AuthService) {
    this.loginGroup = fb.group({
      email: new FormControl('', [
        RxwebValidators.required({}),
        RxwebValidators.email({}),
      ]),
      password: new FormControl('', [
        RxwebValidators.required({}),
        RxwebValidators.password({validation: {
            minLength: 5,

            digit: true
          }}),
      ])
    })
  }

  isDarkTheme() {
    return this.common.currentTheme == 'dark'
  }

  submit() {
    this.triedToSubmit = true;
    if (this.loginGroup.valid) {
      this.auth.login(this.loginGroup.value).subscribe(
        data => {
          if (data.succeeded) {

          } else {
            this.submitErrors = data.errors
            this.common.showToast('error', "Errors", "Errors occurred while registration")
          }
        },
      )
    }
  }
  protected readonly CommonService = CommonService;
}
