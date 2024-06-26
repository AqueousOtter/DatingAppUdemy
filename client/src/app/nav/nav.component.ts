import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';
import { Observable, of } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {

  model:any ={}
  //currentUser$: Observable<User | null> = of(null); // switched to using account service's


  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService){}

  ngOnInit(): void {
    // this.currentUser$ = this.accountService.currentUser$;
  }

  login(){
    
    this.accountService.login(this.model).subscribe({
      next: _ => 
        this.router.navigateByUrl('/members'),
      // error: error => this.toastr.error(error.error) // removed due to interceptor
    })
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/')
    
  }


}
