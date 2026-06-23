import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  error: string = '';

  constructor(private http: HttpClient, private router: Router ) {}

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required])
    });
  }

  onLogin(): void {
    if (this.loginForm.invalid) return;

    this.http.post<any>('http://localhost:5081/api/auth/login', this.loginForm.value)
      .subscribe({
        next: (res) => {
          localStorage.setItem('token', res.token); 
          this.router.navigate(['/dashboard']);     
        },
        error: (err) => {
          this.error = err.error?.message || 'Login failed';
        }
      });
  }
}