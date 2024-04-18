import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from './user';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private authUrl = "http://localhost:5243/api/auth";

  constructor(private http: HttpClient) { }

  register(user: User): Observable<User> {
    return this.http.post<User>(this.authUrl + '/register', user);
  }

  login(user: User): Observable<User> {
    //ocalStorage.setItem("token", user.token);
    return this.http.post<User>(this.authUrl + '/login', user)
  }

  isLoggedIn(): boolean {
    return localStorage.getItem("token") != null;
  }

  logout(): void {
    localStorage.removeItem("token");
  }

}
