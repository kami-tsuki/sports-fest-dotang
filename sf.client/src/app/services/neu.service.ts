import { Injectable } from '@angular/core';

interface User {
  id: number;
  password: string;
  firstName: string;
  lastName: string;
  klass: string;
  group: string;
}

@Injectable({
  providedIn: 'root'
})
export class NeuService {

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
 addUser(user: User): void {
    this.users.push(user);
  }
//// to update users
updateUser(id: number, updatedUser: Partial<User>): void {
  const user = this.users.find(u => u.id === id);
  if (user) {
    Object.assign(user, updatedUser);
  }
}
}
