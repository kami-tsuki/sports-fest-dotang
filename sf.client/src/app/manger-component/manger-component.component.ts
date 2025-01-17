import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AppPaths } from '../app-paths';


@Component({
  selector: 'app-manger-component',
  templateUrl: './manger-component.component.html',
  styleUrls: ['./manger-component.component.css']
})
export class MangerComponentComponent {
  User ={
    id: "",
    firstName: "",
    lastName: "",
  }
  constructor(
    private router: Router){}
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
