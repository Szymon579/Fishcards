import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { User } from '../user';
import { RouterLink } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  isLogin: boolean = true;
  error: boolean = false;
  
  constructor(private authService: AuthService, private router: Router) {}

  displayRegister() : void {
    this.isLogin = !this.isLogin;
  }

  login(email: string, password: string): void {
    if(!email || !password) {
      this.error = true;
      return;
    }

    const user: User = {
      email: email,
      username: email,
      password: password,
      token: ""
    }

    this.authService.login(user).subscribe((user: User) => {
      localStorage.setItem("authToken", user.token);
      console.log("Logged in succesfully!");
      this.router.navigateByUrl("collections");
    });
  }

  register(email: string, password: string, confirmPassword: string): void {
    if(!email || !password || !confirmPassword) {
      this.error = true;
      return;
    }

    if(password !== confirmPassword) {
      this.error = true;
      return;
    }     

    const newUser: User = {
      email: email,
      username: email,
      password: password,
      token: "" 
    };

    console.log(newUser);
    

    this.authService.register(newUser).subscribe(() => this.isLogin = true);

  }
}
