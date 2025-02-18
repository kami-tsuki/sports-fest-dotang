import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AppPaths } from '../app-paths';
import { UsersService, User } from '../services/users.service';



@Component({
  selector: 'app-manger-component',
  templateUrl: './manger-component.component.html',
  styleUrls: ['./manger-component.component.css']
})
export class MangerComponentComponent implements OnInit {
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
deciplien(){
  this.router.navigate([AppPaths.deciplienPage]);
}
}
