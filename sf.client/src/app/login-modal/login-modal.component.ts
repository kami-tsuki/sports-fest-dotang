import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'; 
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { UsersService, User } from '../services/users.service';
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
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.value;

      const result = this.usersService.onSubmit(email, password);

      if (result.success) {
        this.dialogRef.close();

        if (Array.isArray(result.user?.role) && result.user?.role.includes('student')) {
          this.router.navigate([AppPaths.studentPage]);
        } else if (Array.isArray(result.user?.role) && result.user?.role.includes('tutor')) {
          this.router.navigate([AppPaths.teacherPage]);
        } else if (Array.isArray(result.user?.role) && result.user?.role.includes('manager')) {
          this.router.navigate([AppPaths.mangerComponent]);
        } else if (Array.isArray(result.user?.role) && result.user?.role.includes('judge')) {
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
