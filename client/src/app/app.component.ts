import { Component, OnInit } from '@angular/core';
import { User } from './models/user.model';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

    allUsers: User[] | undefined;
    constructor(private accountService: AccountService) { }
  
    ngOnInit(): void {
      this.getLocalStorageCurrentValues();
    }
  
  
    getLocalStorageCurrentValues(): void {
      const userString: string | null = localStorage.getItem('user');
  
      if (userString) {
        const user: User = JSON.parse(userString); // convert string to JSON before sending to method
  
        this.accountService.setCurrentUser(user);
      }
    }
}