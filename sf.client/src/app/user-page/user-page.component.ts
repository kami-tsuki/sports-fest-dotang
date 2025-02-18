import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { UsersService, User } from '../services/users.service';
import { Class } from '../services/class.service';

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
    init: () => {},
    toJSON: () => ({}) 
  }; 
  showAddUserForm: boolean = false;
  showSearchResults: boolean = false;
  selectedUser: User | null = null;
  selectedRole: string = '';
  selectedClass: Class | undefined = undefined;
  allClasses: Class[] = [//TODO I need to replace this hardcoded data with the real data->getClasses() from the service after api finish
    { id: '1', name: 'Class 1', students: [], tutors: [], created: new Date(), updated: new Date(), init: () => {}, toJSON: () => ({}) },
    { id: '2', name: 'Class 2', students: [], tutors: [], created: new Date(), updated: new Date(), init: () => {}, toJSON: () => ({}) }
    
  ]
  

  allUsers = new MatTableDataSource<User>([]);
  allRoles: string[] = ['Student', 'Tutor', 'Manager', 'Judge'];

  // Form fields for search
  searchId: string = '';
  searchName: string = '';
  searchRole: string = '';

  // TheTable columns
  displayedColumns: string[] = ['select','id', 'firstName', 'lastName', 'role', 'class', 'team', 'points'];

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private usersService: UsersService) {}

  ngOnInit(): void {
    this.loadUsers(); // Load users when the component initializes
    //later I might need to load the function of classes from the service getClasses()
  }

  // Load all users from the service
  loadUsers(): void {
    this.users = this.usersService.getUsers();  // Get users from service
    this.allUsers = new MatTableDataSource<User>(this.users);  // Set data source
    this.allUsers.paginator = this.paginator;  // Attach paginator
  }

  // Search users
  searchUsers(): void {
    let filteredUsers = this.users;

    // Filter by ID
    if (this.searchId) {
      filteredUsers = filteredUsers.filter(user => user.id && user.id.includes(this.searchId));
    }

    // Filter by Name
    if (this.searchName) {
      filteredUsers = filteredUsers.filter(user => 
        user.firstName.includes(this.searchName) || user.lastName.includes(this.searchName)
      );
    }

    // Filter by Role
    if (this.selectedRole) {
      filteredUsers = filteredUsers.filter(user => user.role.includes(this.selectedRole));
    }

    // Update the data source to show the filtered list
    this.allUsers.data = filteredUsers;
    this.showSearchResults = !this.showSearchResults;
  }

  // To choose the user for editing
  onCheckboxChange(user: User, event: Event): void {
  const isChecked = (event.target as HTMLInputElement).checked;
  this.onSelectUser(user, isChecked);
}

  onSelectUser(user: User | null, isChecked: boolean): void {
    if (!user) {
      console.error('No user selected');
      return;
    }
  
    // Proceeding
    if (isChecked) {
      this.selectedUser = { 
        ...user,
        init: user?.init || (() => {}),  // to avoid the error this is default implementation of init if user is undefined
        toJSON: user?.toJSON || (() => ({}))  // also same here default implementation of toJSON if user is undefined
      };
    } else {
      this.selectedUser = null; // Clear selection if unchecked
    }
  }
  
  
  // To save after editing
  saveUser(): void {
    if (this.selectedUser && this.selectedUser.id) { // to check if id is defined--i might edit it
      this.selectedUser.role = this.selectedRole;
      this.selectedUser.class = this.selectedClass;
      this.usersService.updateUser(this.selectedUser.id, this.selectedUser); // Update user via the service
      this.users = this.usersService.getUsers(); // to refresh the users list
      this.allUsers.data = [...this.users]; // Update the data source
      console.log('User updated successfully:', this.selectedUser);
      this.selectedUser = null; // here i want to clear the selection after finishing
    } else {
      console.error('Cannot update user: ID is missing.');
      alert('Cannot update user: ID is missing.');
    }
  }
  // this function to get the class names of the user, i allowed multiple classes for the user
  getClassNames(user: User): string {
    return Array.isArray(user.class) ? user.class.map(c => c.name).join(', ') : 'No classes';
  }
  
  // To delete after editing
  deleteUser(): void {
    if (this.selectedUser && this.selectedUser.id) { // to check if id is defined--i might edit it
      this.usersService.deleteUser(this.selectedUser.id); // Update user via the service
      this.users = this.usersService.getUsers(); // to refresh the users list
      this.allUsers.data = [...this.users]; // Update the data source
      console.log('User Deleted successfully:', this.selectedUser);
      this.selectedUser = null; // here i want to clear the selection after finishing
    } else {
      console.error('Cannot update user: ID is missing.');
      alert('Cannot Delete user: ID is missing.');
    }
  }
  
  

  // Add new user
  addUser(user: User): void {
    try {
      user.role = this.selectedRole;
      user.class = this.selectedClass;
      this.usersService.addUser(user); 
      this.users = this.usersService.getUsers(); // this is to fetch the updated list
      this.allUsers.data = [...this.users]; // and here to update data source
  
      // Reset the newUser object
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
        init: () => {}, // I might use it to implement some default actions
        toJSON: () => ({})
      };
      this.selectedRole = ''; // Clear the selected role
  
      // Display success message
      alert('User added successfully!');
      console.log('User added successfully', this.users);
      this.showAddUserForm = false;
    } catch (error) {
      console.error('Error adding user:', error);
      alert('Failed to add user. Please try again.');
    }
  }
  

  // Toggle the add user form
  toggleAddUserForm(): void {
    this.showAddUserForm = !this.showAddUserForm;
  }
}
