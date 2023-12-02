import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { RegisterUser } from 'src/app/models/register-user-model';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  passwordsNotMatch: boolean | undefined;
  apiErrorMassage: string | undefined;


  constructor(private accountService: AccountService, private fb: FormBuilder, private router: Router) { }


  //#region geter Form Group
  registerFg = this.fb.group({
    firstNameCtrl: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30)]],
    lastNameCtrl: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30)]],
    userNameCtrl: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(10)]],
    phoneNumberCtrl: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(13)]],
    emailCtrl: ['', [Validators.pattern(/^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$/)]],
    passwordCtrl: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(16)]],
    confirmPasswordCtrl: ['', Validators.required, Validators.minLength(8),  Validators.maxLength(16)]
  });

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
  //#endregion geter Form Group

  //#region Methods
  register(): void {
    this.apiErrorMassage = undefined;

    if (this.PasswordCtrl.value === this.ConfirmPasswordCtrl.value) {
      this.passwordsNotMatch = false;

      let user: RegisterUser = {
        firstName: this.FirstNameCtrl.value,
        lastName: this.LastNameCtrl.value,
        userName: this.UserNameCtrl.value,
        phoneNumber: this.PhoneNumberCtrl.value,
        email: this.EmailCtrl.value,
        password: this.PasswordCtrl.value,
        confirmPassword: this.ConfirmPasswordCtrl.value,
      }

      // return: Observable<User>
      this.accountService.registerUser(user).subscribe({
        next: user => {
          console.log(user);
          this.router.navigateByUrl('/');
        }, 
        error: err => this.apiErrorMassage = err.error
      })

    }
    else {
      this.passwordsNotMatch = true;
    }

    console.log(this.registerFg.value);

    this.registerFg.reset();
  }

  clearForm(): void {
    this.registerFg.reset();
  }

  getState(): void {
    console.log(this.registerFg);
  }
  //#endregion Methods
}