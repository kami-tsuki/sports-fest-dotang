import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { UsersService } from '../services/users.service';

interface User {
  id: number;
  firstName: string;
  lastName: string;
  role: string;
  klass?: string;
  team?: string;
  points?: number;
}

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css']
})
export class UserPageComponent implements OnInit {
  // User list and data source
  users: User[] = [];
  allUsers = new MatTableDataSource<User>([]);

  // Form fields for search
  searchId: string = '';
  searchName: string = '';
  searchRole: string = '';

  // Table columns
  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'role', 'klass', 'team'];

  @ViewChild(MatPaginator) paginator!: MatPaginator; // Paginator reference

  constructor(private usersService: UsersService) {}

  ngOnInit(): void {
    this.loadUsers(); // Load users when the component initializes
  }

  // Load all users from the service
  loadUsers(): void {
    this.users = this.usersService.getUsers(); // Replace with API call if necessary
    this.allUsers.data = this.users; // Populate the table data source
    this.allUsers.paginator = this.paginator; // Connect paginator
  }

  // Search users based on input fields
  searchUsers(): void {
    const filteredUsers = this.users.filter(user => {
      const matchesId = this.searchId ? user.id.toString().includes(this.searchId) : true;
      const matchesName = this.searchName
        ? `${user.firstName} ${user.lastName}`.toLowerCase().includes(this.searchName.toLowerCase())
        : true;
      const matchesRole = this.searchRole ? user.role.toLowerCase().includes(this.searchRole.toLowerCase()) : true;
      return matchesId && matchesName && matchesRole;
    });

    // Update the table's data source with the filtered data
    this.allUsers.data = filteredUsers;
  }
}
