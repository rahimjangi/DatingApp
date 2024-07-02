import { Component, OnInit, inject } from '@angular/core';
import { RegisterComponent } from '../register/register.component';
import { HttpClient } from '@angular/common/http';
import { User } from '../_models/user.model';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  imports: [RegisterComponent],
})
export class HomeComponent implements OnInit {
  registerMode = false;
  http = inject(HttpClient);
  users: any;

  registerToggle() {
    this.registerMode = !this.registerMode;
  }
  ngOnInit(): void {
    this.getUsers();
  }
  getUsers() {
    this.http.get<User[]>('https://localhost:5001/api/users').subscribe({
      next: (response) => {
        this.users = response;
      },
      error: (err) => {
        console.log(err);
      },
      complete: () => {
        console.log('request has completed');
      },
    });
  }
  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}
