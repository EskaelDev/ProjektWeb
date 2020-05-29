import { Component, OnInit } from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {User} from '../_models/user';
import {MustMatch} from '../_helpers/must-match';
import {AuthenticationService} from '../_services/authentication-service';
import {ActivatedRoute, Router} from '@angular/router';
import {first} from 'rxjs/operators';

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
  returnUrl: string;
  error = '';
  loading = false;

  constructor(private fb: FormBuilder, private route: ActivatedRoute,
              private router: Router, private authenticationService: AuthenticationService) {
    // redirect to home if already logged in
    if (this.authenticationService.currentUserValue) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit() {
    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  get f() { return this.loginForm.controls; }
  get c() { return this.createForm.controls; }

  login() {
    this.loading = true;
    this.authenticationService.login(this.f.email.value, this.f.password.value)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate([this.returnUrl]);
          },
        error => {
          this.error = error;
          this.loading = false;
          console.log(error);          
        });
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
