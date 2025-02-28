import {Component, OnInit} from '@angular/core';
import {UsersService, User} from '../services/users.service';


@Component({
    selector: 'app-student-page',
    templateUrl: './student-page.component.html',
    styleUrl: './student-page.component.css'
})
export class StudentPageComponent implements OnInit {
    //ToDo-->following data are required
    loggedInUser: User | null = null;

    constructor(
        private usersService: UsersService
    ) {
    }

    ngOnInit(): void {
        this.loggedInUser = this.usersService.getLoggedInUser();
    }

    teacherContact(): void {
        window.location.href = 'mailto:teacher@ovg-bbs.com?subject=Support Request&body=Sehr geehrte Herr Lehrer,%0D%0A%0D%0A';
    }
}