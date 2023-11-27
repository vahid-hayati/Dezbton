import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginUser } from 'src/app/models/login-user.models';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  apiErrorMessage: string | undefined;

  constructor(private accountService: AccountService, private fb: FormBuilder, private ruoter: Router) { }

  //#region FormGroup
  loginFg = this.fb.group({
    userNameCtrl: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(10)]],
    passwordCtrl: ['', [Validators.required, Validators.minLength(8)]]
  });

  get UserNameCtrl(): FormControl {
    return this.loginFg.get('userNameCtrl') as FormControl;
  }

  get PasswordCtrl(): FormControl {
    return this.loginFg.get('passwordCtrl') as FormControl;
  }
  //#endregion FormGroup

  //#region Methods
  login(): void {
    this.apiErrorMessage = undefined;

    let user: LoginUser = {
      userName: this.UserNameCtrl.value,
      password: this.PasswordCtrl.value
    }

    //return: Observable<LoggedInUser>
    this.accountService.loginUser(user).subscribe({
      next: user => {
        console.log(user);
        this.ruoter.navigateByUrl('/');
      },
      error: err => this.apiErrorMessage = err.error
    });
  }

  getState(): void {
    console.log(this.loginFg);
  }
  //#endregion Methods
}