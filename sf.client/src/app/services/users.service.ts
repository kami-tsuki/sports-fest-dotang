import { Injectable } from '@angular/core';
import {Class} from "@app/services/class.service";
import {Team} from "@app/services/team.service";
import {EntityOfGuid} from "@app/services/api/sf-client";

export interface User extends EntityOfGuid {
  password: string;
  firstName: string;
  lastName: string;
  email: string;
  role: string[];
  class?: Class[];
  team?: Team; 
  points?: number; 
}

@Injectable({
  providedIn: 'root'  
})
export class UsersService {

  private users: User[] = [];
  private loggedInUser: User | null = null;

  constructor() {}
//// to get all users
  getUsers(): User[] {
    return this.users;
  }
//// to get user name
getUserByUsername(firstName: string): User | undefined {
  return this.users.find(user => user.firstName === firstName);
}
//// to check password
onSubmit(email: string, password: string): { success: boolean, message: string, user?: User } {
  const user = this.users.find(u => u.email === email);
  if (user && user.password === password) {
    this.loggedInUser = user;
    localStorage.setItem('user', JSON.stringify(user));
    return {
      success: true,
      message: 'Login successful!',
      user,
    };
  } else {
    return {
      success: false,
      message: 'Invalid username or password'
    };
  }
}
getLoggedInUser(): User | null {
  return this.loggedInUser || JSON.parse(localStorage.getItem('user') || 'null');}
  
/// To log out- i might move it to the nav-bar component
logout(): void {
  this.loggedInUser = null;
  localStorage.removeItem('user');
}

//// to add neu users
 addUser(newUser: User): void {
    this.users.push(newUser);
  }
//// to update users
updateUser(id: string, updatedUser: Partial<User>): void {
  const user = this.users.find(u => u.id === id);
  if (user) {
    Object.assign(user, updatedUser);
  }
}
//// Delete a user
deleteUser(id: string): void {
  this.users = this.users.filter(user => user.id !== id);
}
}
