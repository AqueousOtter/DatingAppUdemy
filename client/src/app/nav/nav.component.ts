import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {

  model:any ={}
  //currentUser$: Observable<User | null> = of(null); // switched to using account service's


  constructor(public accountService: AccountService){}

  ngOnInit(): void {
    // this.currentUser$ = this.accountService.currentUser$;
  }

  login(){
    
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response);
      },
      error: error => console.log(error)
    })
  }

  logout() {
    this.accountService.logout();
    
  }


}
