import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginUser } from 'src/app/models/login-user.models';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  resLogin: LoginUser | undefined;

  constructor(private fb: FormBuilder, private http: HttpClient, private ruoter: Router) { }

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

  loginUser(): void {
    this.http.get<LoginUser>('http://localhost:5000/api/user/get-by-userName'
      + this.UserNameCtrl.value + '/' + this.PasswordCtrl.value).subscribe(
        {
          next: res => {
            this.resLogin = res
            this.ruoter.navigateByUrl('');
          }

        }
      )
  }
}
