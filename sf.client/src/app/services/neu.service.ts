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
