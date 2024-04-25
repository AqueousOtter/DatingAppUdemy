import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title: string = 'Demo App Udemy';

  users: any;

  constructor(private http: HttpClient, private accountService: AccountService){}

  ngOnInit(): void {
    this.getUsers();
    this.setCurrentUser(); //sets if we have a user in local storages
  }

  getUsers(){
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log('Request is finished')
    })
  }
  setCurrentUser(){
   // const user: User = JSON.parse(localStorage.getItem('user')!); // this way overrides type safety, its okay now but we can be explicit
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);


  }

}
