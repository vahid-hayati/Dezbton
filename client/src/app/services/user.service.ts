import { Injectable } from '@angular/core';
import { AccountService } from './account.service';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, private accountService: AccountService) { }

  getAllUsers(): Observable<User[] | null> {
    return this.http.get<User[]>('https://localhost:5001/api/user').pipe(
      map((users: User[]) => {
        if (users)
          return users;

        return null;
      })
    )
  }

  getUserByPhoneNumber(): Observable<User | null> {
    return this.http.get<User>('https://localhost:5001/api/user/get-by-phoneNumber/09197852024').pipe(
      map((user: User | null) => {
        if (user)
          return user;

        return null;
      })
    )
  }
}
