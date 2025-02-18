import {Injectable} from '@angular/core';
import {Class} from "@app/services/class.service";
import {Team} from "@app/services/team.service";
import {EntityOfGuid} from "@app/services/api/sf-client";
import {ApiService} from "@app/services/api.service";

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

    private loggedInUser: User | null = null;

    constructor(
        private apiService: ApiService
    ) {
    }


//// to get all users
    getUsers(): User[] {
        let users: User[] = [];
        this.apiService.get<User[]>('users').subscribe(u => {
            users = u;
        });
        return users;
    }


    getLoggedInUser(): User | null {
        return this.loggedInUser || JSON.parse(localStorage.getItem('user') || 'null');
    }

/// To log out- i might move it to the nav-bar component
    logout(): void {
        this.loggedInUser = null;
        localStorage.removeItem('user');
    }

    getUserByUsername(firstName: string): User | undefined {
        let user: User | undefined;
        this.apiService.get<User[]>('users').subscribe(u => {
            user = u.find(u => u.firstName === firstName);
        });
        return user;
    }

//// to check password, //TODO: rename to "login" since OnSubmit dont explains its use
    onSubmit(username: string, password: string): { success: boolean, message: string, user?: User } {
        let user: User | undefined;

        this.apiService.get<User[]>('users').subscribe(u => {
            //TODO we just have first and lastname, but no username... pls rethink this
            user = u.find(u => u.firstName + " " + u.lastName === username);
        });
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

//// to add new a user
    addUser(newUser: User): void {
        this.apiService.post<User>('users', newUser);
    }

//// to update users
    updateUser(id: string, updatedUser: Partial<User>): void {
        this.apiService.get<User[]>('users').subscribe(u => {
            let user = u.find(u => u.id === id);
            if (user) {
                Object.assign(user, updatedUser);
            } else {
                console.error(`UsersService: updateUser failed to find user with id ${id}`);
            }
            this.apiService.put<User>('users', user);
        });

    }

//// Delete a user
    deleteUser(id: string): void {
        this.apiService.get<User[]>('users').subscribe(u => {
            let user = u.find(u => u.id === id);
            if (user) {
                this.apiService.delete<User>(`users/${user.id}`);
            } else {
                console.error(`UsersService: deleteUser failed to find user with id ${id}`);
            }
        });
    }
}
