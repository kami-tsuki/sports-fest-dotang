import {Component} from '@angular/core';
import {MatDialogRef} from '@angular/material/dialog';
import {ApiService} from '../../../services/api.service';
import {IResultModelOfUser, IUser, RoleType} from '../../../services/api/sf-client';

@Component({
  selector: 'sf-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent {
  public firstName: string = '';
  public lastName: string = '';
  public email: string = '';
  public role: RoleType = RoleType.User

  constructor(
      private api: ApiService,
      public dialogRef: MatDialogRef<CreateUserComponent>
  ) {}

  createUser() {
    const newUser: Partial<IUser> = {
      first: this.firstName,
      last: this.lastName,
      email: this.email,
      role: this.role
    };

    this.api.post<IResultModelOfUser>('/api/v1/user', newUser).subscribe(
        (result) => {
          if (result.success) {
            this.dialogRef.close(result.data);
          } else {
            console.warn('Failed to create user');
          }
        }
    );
  }

  close() {
    this.dialogRef.close();
  }

  protected readonly RoleType = RoleType;
}