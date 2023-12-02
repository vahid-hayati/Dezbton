import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user.model';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  user: User | null | undefined

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: response => this.user = response
    })
  }

  logout(): void {
    this.accountService.logoutUser();
  }
}