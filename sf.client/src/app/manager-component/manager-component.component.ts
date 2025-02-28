import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AppPaths } from '../app-paths';
import { UsersService, User } from '../services/users.service';



@Component({
  selector: 'app-manager-component',
  templateUrl: './manager-component.component.html',
  styleUrls: ['./manager-component.component.css']
})
export class ManagerComponentComponent implements OnInit {
    //ToDo-->following data are required
    loggedInUser: User | null = null;

    constructor(
      private usersService: UsersService,
      private router: Router){}
      ngOnInit(): void {
        this.loggedInUser = this.usersService.getLoggedInUser();
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
discipline(){
  this.router.navigate([AppPaths.disciplinePage]);
}
}
