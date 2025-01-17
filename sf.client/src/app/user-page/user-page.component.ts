import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { UsersService, User } from '../services/users.service'; // Import User from the service

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css']
})
export class UserPageComponent implements OnInit {
  // User list and data source
  users: User[] = [];
  newUser: User = {
    id: '', 
    password: '', 
    firstName: '', 
    lastName: '', 
    role: '', 
    class: undefined, 
    team: undefined, 
    points: 0, 
    created: new Date(), 
    updated: new Date(),
    init: () => {}, // Reset default `init`
    toJSON: () => ({}) 
  }; 
  showAddUserForm: boolean = false;

  allUsers = new MatTableDataSource<User>([]);

  // Form fields for search
  searchId: string = '';
  searchName: string = '';
  searchRole: string = '';

  // TheTable columns
  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'role', 'class', 'team', 'points'];

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private usersService: UsersService) {}

  ngOnInit(): void {
    this.loadUsers(); // Load users when the component initializes
  }

  // Load all users from the service
  loadUsers(): void {
    this.users = this.usersService.getUsers();
    this.allUsers.data = this.users; // Populate the table data source
    this.allUsers.paginator = this.paginator;
  }

  // Search users
  searchUsers(): void {
    const filteredUsers = this.users.filter(user => {
      const matchesId = this.searchId ? user.id?.includes(this.searchId) : true; // Safely access id caus i had error here
      const matchesName = this.searchName
        ? `${user.firstName} ${user.lastName}`.toLowerCase().includes(this.searchName.toLowerCase())
        : true;
      const matchesRole = this.searchRole ? user.role.toLowerCase().includes(this.searchRole.toLowerCase()) : true;
      return matchesId && matchesName && matchesRole;
    });

    // Update the table data source with the filtered data
    this.allUsers.data = filteredUsers;
  }

  // Add new user
  addUser(user: User): void {
    this.usersService.addUser(user); // Add user via service
    this.users = this.usersService.getUsers(); // Fetch updated list
    this.allUsers.data = [...this.users]; // Update data source
    this.newUser = {
      id: '', 
      password: '', 
      firstName: '', 
      lastName: '', 
      role: '', 
      class: undefined, 
      team: undefined, 
      points: 0, 
      created: new Date(), 
      updated: new Date(),
      init: () => {}, // Default implementation for init/ I need to check it agian
      toJSON: () => ({})
    };
    this.showAddUserForm = false;
  }

  // Toggle the add user form
  toggleAddUserForm(): void {
    this.showAddUserForm = !this.showAddUserForm;
  }
}
