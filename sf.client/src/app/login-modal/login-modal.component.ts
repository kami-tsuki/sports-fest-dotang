import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'; 
import { MatDialogRef } from '@angular/material/dialog';
import { Router,ActivatedRoute } from '@angular/router';
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
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const { username, password } = this.loginForm.value;

/*ToDo: instead of the fixed username and password i wrote inside the method
we need to create api here to check data and also the role of the user to route him
to the right page[Manager, teacher or student]
*/
      // check username and password
      if (username.toLowerCase() === 'student' && password === '123') {
        this.dialogRef.close(); // closing the modal
        // redirect to the student page
        this.router.navigate([AppPaths.studentPage]); // navigate to student page
      } 
      if (username.toLowerCase() === 'teacher' && password === '123') {
        this.dialogRef.close(); // close the modal
        // redirect to the student page
        this.router.navigate([AppPaths.teacherPage]); // navigate to student page
      } 
      if (username.toLowerCase() === 'manager' && password === '123') {
        this.dialogRef.close(); // close the modal
        // redirect to the student page
        this.router.navigate([AppPaths.mangerComponent]); // navigate to student page
      } 
      else {
        alert('Username or password is not recognized.');
      }
    } else {
      console.log('Form is invalid!');
    }
  }
}
