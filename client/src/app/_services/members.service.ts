import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseURL = environment.apiURL;

  constructor(private http: HttpClient) { }


  getMembers(){
    return this.http.get<Member[]>(this.baseURL + 'users') //, this.getHttpOptions()
  }

  getMember(username: string){
    return this.http.get<Member>(this.baseURL + 'users/' + username) //, this.getHttpOptions()
  }

  // getHttpOptions() {
  //   const userString = localStorage.getItem('user');

  //   if(!userString) return;


  //   const user = JSON.parse(userString);
  //   return{
  //     headers: new HttpHeaders({
  //       Authorization: 'Bearer ' + user.token
  //     })
  //   }
  // }
}
