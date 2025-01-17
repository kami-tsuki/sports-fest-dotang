import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'; 
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { UsersService } from '../services/users.service';
import { AppPaths } from '../app-paths';

@Component({
  selector: 'app-login-modal',
  templateUrl: './login-modal.component.html',
  styleUrls: ['./login-modal.component.css']
})
export class LoginModalComponent {
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder, 
    private dialogRef: MatDialogRef<LoginModalComponent>,
    private router: Router,
    private usersService: UsersService
  ) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const { username, password } = this.loginForm.value;

      const result = this.usersService.onSubmit(username, password);

      if (result.success) {
        this.dialogRef.close();

        if (result.user?.role.toLowerCase() === 'student') {
          this.router.navigate([AppPaths.studentPage]);
        } else if (result.user?.role.toLowerCase() === 'teacher') {
          this.router.navigate([AppPaths.teacherPage]);
        } else if (result.user?.role.toLowerCase() === 'manager') {
          this.router.navigate([AppPaths.mangerComponent]);
        }else if (result.user?.role.toLowerCase() === 'judge') {
          this.router.navigate([AppPaths.judgeComponent]);
        }
      } else {
        alert(result.message);
      }
    } else {
      console.log('Form is invalid!');
    }
  }
}
