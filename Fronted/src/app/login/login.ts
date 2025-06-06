import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  standalone:false,
  templateUrl: './login.html',
  styleUrls: ['./login.css']
})
export class LoginComponent {
  loginData = {
    usernameOrEmail: '',
    password: ''
  };

  constructor(private http: HttpClient) {}

  onLogin() {
    this.http.post('https://localhost:7217/api/User/login', this.loginData).subscribe({
      next: (res: any) => {
        alert('Login successful!');
        console.log(res); // Optional: save token or userId here
      },
      error: (err) => {
        alert('Login failed: ' + err.error);
      }
    });
  }
}
