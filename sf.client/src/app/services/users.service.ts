import { Injectable } from '@angular/core';
import {Class} from "@app/services/class.service";
import {Team} from "@app/services/team.service";
import {EntityOfGuid} from "@app/services/api/sf-client";

export interface User extends EntityOfGuid {
  password: string;
  firstName: string;
  lastName: string;
  role: string;
  class?: Class;
  team?: Team; 
  points?: number; 
}

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  private users: User[] = [];

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
onSubmit(username: string, password: string): { success: boolean, message: string, user?: User } {
  const user = this.users.find(u => u.firstName === username);
  if (user && user.password === password) {
    return {
      success: true,
      message: 'Login successful!',
      user
    };
  } else {
    return {
      success: false,
      message: 'Invalid username or password'
    };
  }
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
