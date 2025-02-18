import {Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import {ClassesService, Class} from '@app/services/class.service';
import {User, UsersService} from '@app/services/users.service';

@Component({
    selector: 'app-class-page',
    templateUrl: './class-page.component.html',
    styleUrls: ['./class-page.component.css']
})
export class ClassPageComponent {
    users: User[] = [];
    classes: Class[] = [];
    newClass: Class = {
        id: '',
        name: '',
        students: [],
        tutors: [],
        created: new Date(),
        updated: new Date(),
        init: () => {
        },
        toJSON: () => ({})
    };
    name: string = '';
    showSearchResults: boolean = false;
    showAddClassForm: boolean = false;
    selectedClass: Class | null = null;
    searchName: string = '';
    allUsers = new MatTableDataSource<User>([]);
    displayedColumns: string[] = ['select', 'id', 'name', 'students', 'tutors'];
    @ViewChild(MatPaginator) paginator!: MatPaginator;


    constructor(private classesService: ClassesService, private usersService: UsersService) {
    }

    //because im depending on users data i used this function
    loadUsers(): void {
        this.users = this.usersService.getUsers();  // Get users from service
        this.allUsers = new MatTableDataSource<User>(this.users);  // Set data source
        this.allUsers.paginator = this.paginator;  // Attach paginator
    }

    ngOnInit(): void {
        this.loadUsers(); // Load users when the component initializes
    }

    //Get all classes from service file
    getClasses(): void {
        this.classes = this.classesService.getClasses();
    }

    // Search users
    searchClass(): void {
        let filteredClass = this.users;
        // Filter by Name
        if (this.searchName) {
            filteredClass = filteredClass.filter(user =>
                user.class?.name?.toLowerCase().includes(this.searchName.toLowerCase())
            );
        }
        // Update the data source to show the filtered list
        this.allUsers.data = filteredClass;
        this.showSearchResults = !this.showSearchResults;
    }

    toggleAddClassForm(): void {
        this.showAddClassForm = !this.showAddClassForm;
    }

    addClass(): void {
        this.classesService.addClass(this.newClass);
        this.newClass = {
            id: '',
            name: '',
            students: [],
            tutors: [],
            created: new Date(),
            updated: new Date(),
            init: () => {
            },
            toJSON: () => ({})
        };
        this.showAddClassForm = false;
    }

    saveClass(): void {
        if (this.selectedClass) {
            if (this.selectedClass && this.selectedClass.id) {
                this.classesService.updateClass(this.selectedClass.id, this.selectedClass);
            }
        }
    }

    deleteClass(): void {
        this.classesService.deleteClass(this.selectedClass?.id || '');
        this.loadUsers();
    }
}