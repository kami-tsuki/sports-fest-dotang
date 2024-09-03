import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { IResultModelOfPageOfUser, IUser } from '../../../services/api/sf-client';
import { ApiService } from '../../../services/api.service';
import { CreateUserComponent } from '../create-user/create-user.component';

@Component({
    selector: 'sf-select-user',
    templateUrl: './select-user.component.html',
    styleUrls: ['./select-user.component.css']
})
export class SelectUserComponent implements OnInit {
    public users: IUser[] = [];
    public query: string = '';
    public selectedUser: IUser | undefined;

    constructor(
        private api: ApiService,
        public dialogRef: MatDialogRef<SelectUserComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any,
        private dialog: MatDialog
    ) {}

    ngOnInit(): void {
        this.loadUsers();
    }

    loadUsers() {
        if (this.query && this.query.length > 0) {
            this.api.get<IResultModelOfPageOfUser>(`/api/v1/user/search?query=${this.query}`).subscribe(
                (result) => {
                    if (result.success) {
                        this.users = result.data?.data || [];
                    } else {
                        console.warn('Failed to load users');
                    }
                }
            );
        } else {
            this.api.get<IResultModelOfPageOfUser>(`/api/v1/user`).subscribe(
                (result) => {
                    if (result.success) {
                        this.users = result.data?.data || [];
                    } else {
                        console.warn('Failed to load users');
                    }
                }
            );
        }
    }

    selectUser(user: IUser) {
        this.selectedUser = user;
        localStorage.setItem('selectedUser', JSON.stringify(user));
        this.dialogRef.close(user);
    }

    isSelected(user: IUser) {
        return this.selectedUser?.id === user.id;
    }

    close() {
        this.dialogRef.close();
    }

    openCreateUserModal() {
        const dialogRef = this.dialog.open(CreateUserComponent, {
            width: '400px'
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                this.loadUsers();
            }
        });
    }
}