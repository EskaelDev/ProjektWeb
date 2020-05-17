import { Component, OnInit } from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {User} from '../model/user';
import {MustMatch} from '../services/must-match';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });

  createForm = this.fb.group({
    name: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required]
  }, {
    validator: MustMatch('password', 'confirmPassword')
  });

  hidePassword = true;
  hideConfirmPassword = true;
  isLoginForm = true;

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
  }

  get f() { return this.loginForm.controls; }
  get c() { return this.createForm.controls; }

  login() {
    // this.authenticationService.login(this.f.username.value, this.f.password.value)
    //   .pipe(first())
    //   .subscribe(
    //     data => {
    //       this.router.navigate([this.returnUrl]);
    //     },
    //     error => {
    //       this.error = error;
    //       this.loading = false;
    //     });
  }

  save() {
    const user: User = {
      name: this.c.name.value,
      email: this.c.email.value,
      password: this.c.password.value
    };
  }

  arePasswordsTheSame(): boolean {
    return this.c.password.value === this.c.confirmPassword.value;
  }

  signUpClicked() {
    this.isLoginForm = false;
  }

  signInClicked() {
    this.isLoginForm = true;
  }

  isFormValid() {
    return this.arePasswordsTheSame() && this.createForm.valid;
  }
}
