import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { RegisterUser } from 'src/app/models/register-user-model';
import { Register } from 'src/app/models/register.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  userRes: RegisterUser | undefined;
  

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  //#region Create Form Group/controler (AbstractControl)
  registerFg = this.fb.group({
    firstNameCtrl: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30)]],
    lastNameCtrl: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30)]],
    userNameCtrl: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(10)]],
    phoneNumberCtrl: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(13)]],
    emailCtrl: ['', [Validators.pattern(/^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$/)]],
    passwordCtrl: ['', [Validators.required, Validators.minLength(8)]],
    confirmPasswordCtrl: ['', Validators.required, Validators.minLength(8)]
  });
  //#endregion

  //#region geter Form Group
  get FirstNameCtrl(): FormControl {
    return this.registerFg.get('firstNameCtrl') as FormControl;
  }

  get LastNameCtrl(): FormControl {
    return this.registerFg.get('lastNameCtrl') as FormControl;
  }

  get UserNameCtrl(): FormControl {
    return this.registerFg.get('userNameCtrl') as FormControl;
  }

  get PhoneNumberCtrl(): FormControl {
    return this.registerFg.get('phoneNumberCtrl') as FormControl;
  }

  get EmailCtrl(): FormControl {
    return this.registerFg.get('emailCtrl') as FormControl;
  }

  get PasswordCtrl(): FormControl {
    return this.registerFg.get('passwordCtrl') as FormControl;
  }

  get ConfirmPasswordCtrl(): FormControl {
    return this.registerFg.get('confirmPasswordCtrl') as FormControl;
  }
  //#endregion

  registerUser(): void {
    console.log(this.registerFg.value);

    let user: Register = {
      firstName: this.FirstNameCtrl.value,
      lastName: this.LastNameCtrl.value,
      userName: this.UserNameCtrl.value,
      phoneNumber: this.PhoneNumberCtrl.value,
      email: this.EmailCtrl.value,
      password: this.PasswordCtrl.value,
      confirmPassword: this.ConfirmPasswordCtrl.value,
    }

    this.http.post<RegisterUser>('http://localhost:5000/api/user/register', user).subscribe(
      {
        next: response => {
          // this.userRes = response;
          this.userRes = response;
          console.log(this.userRes);

        }
      }
    );

    this.registerFg.reset();
  }

  clearForm(): void {
    this.registerFg.reset();
  }
}