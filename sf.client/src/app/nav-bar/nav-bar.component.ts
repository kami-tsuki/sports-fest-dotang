import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { LoginModalComponent } from '../login-modal/login-modal.component';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';


@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent implements OnInit  {

  isStudentPage: boolean = false;
  isTeacherPage: boolean = false;
  constructor(
    private dialog: MatDialog,
    private activatedRoute: ActivatedRoute,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        const currentUrl = this.router.url;
        this.isStudentPage = currentUrl.includes('/studentPage');
        this.isTeacherPage = currentUrl.includes('/teacherPage');

      }
    });
  }

  openLoginModal(): void {
    this.dialog.open(LoginModalComponent, {
      width: '400px',  // this is Optional to Set dialog width
    });
  }
    logout(): void {  
      // Redirect to the desired page after logout
      this.router.navigate(['/']);
    }
  }