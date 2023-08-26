import { Component } from '@angular/core';
import {AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators} from "@angular/forms";
import {RxwebValidators} from "@rxweb/reactive-form-validators";
import {TranslatePipe} from "@ngx-translate/core";
import {CommonService} from "../../AppCommon/common.service";
import {AuthService} from "../auth.service";
import {Result} from "../../models/Result";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  registerGroup: FormGroup;
  triedToSubmit: boolean = false;

  submitErrors: string[] | undefined;

  passwordRules =  {
    minLength: 5,
    upperCase: true,
    lowerCase: true,
    digit: true
  }

  constructor(fb: FormBuilder,
              private translatePipe: TranslatePipe,
              private auth: AuthService,
              private common: CommonService) {
    this.registerGroup = fb.group({
      email: new FormControl('', [
        RxwebValidators.required({}),
        RxwebValidators.email({})
      ]),
      username: new FormControl('', [
        RxwebValidators.required({}),
        RxwebValidators.minLength({value:3})
      ]),
      password: new FormControl('', [
        RxwebValidators.required({}),
        passwordValidator(),
        RxwebValidators.minLength({value: this.passwordRules.minLength})
      ]),
      confirmPassword: new FormControl('', [
        RxwebValidators.required({}),
        RxwebValidators.compare({fieldName: 'password'})
      ])
    })
  }

  get email() {
    return this.registerGroup.get('email')
  }

  get username() {
    return this.registerGroup.get('username')
  }

  get password() {
    return this.registerGroup.get('password')
  }

  get confirmPassword() {
    return this.registerGroup.get('confirmPassword')
  }

  translatePipeArgument(fieldName: string) {
    return this.translatePipe.transform(fieldName);
  }

  submit() {
    this.triedToSubmit = true;
    if (this.registerGroup.valid) {
      this.auth.register(this.registerGroup.value).subscribe(
        (data: Result) => {
          if (data.succeeded) {
            this.common.showToast('success', "Ok", "")
          } else {
            this.submitErrors = data.errors;
            this.common.showToast('error', "Errors", "Errors occurred while registration")
          }
        }
      )
    }
  }

  protected readonly CommonService = CommonService;
}

export function passwordValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: boolean } | null => {
    const value = control.value;

    const hasUpperCase = /[A-Z]/.test(value);
    const hasLowerCase = /[a-z]/.test(value);
    const hasDigit = /\d/.test(value);

    if (!hasUpperCase) {
      return { uppercaseRequired: true };
    }

    if (!hasLowerCase) {
      return { lowercaseRequired: true };
    }

    if (!hasDigit) {
      return { digitRequired: true };
    }

    return null;
  };
}
