import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppPaths } from '../app-paths';
import { UsersService, User } from '../services/users.service';


@Component({
  selector: 'app-teacher-page',
  templateUrl: './teacher-page.component.html',
  styleUrl: './teacher-page.component.css'
})
export class TeacherPageComponent implements OnInit {
    //ToDo-->following data are required
    loggedInUser: User | null = null;

    constructor(
      private usersService: UsersService,
      private router: Router){}
      ngOnInit(): void {
        this.loggedInUser = this.usersService.getLoggedInUser();
      }
      contactManager(): void {
        window.location.href = 'mailto:manager@ovg-bbs.com?subject=Support Request&body=Sehr geehrte Herr Manager,%0D%0A%0D%0A';
      }
      

  user(){
    this.router.navigate([AppPaths.userPage]);
  }
  team(){
    this.router.navigate([AppPaths.teamPage]);
  }
  class(){
    this.router.navigate([AppPaths.classPage]);
  }
  deciplien(){
    this.router.navigate([AppPaths.deciplienPage]);
  }
}
