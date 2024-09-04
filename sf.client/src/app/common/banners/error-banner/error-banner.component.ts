import { Component, Input, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-error-banner',
  templateUrl: './error-banner.component.html',
  styleUrls: ['./error-banner.component.css']
})
export class ErrorBannerComponent implements OnInit {
  @Input() message: string = '';
  @Input() autoDismiss: boolean = true;
  @Input() dismissTime: number = 5000; 

  constructor(private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    if (this.autoDismiss) {
      setTimeout(() => this.dismiss(), this.dismissTime);
    }
  }

  dismiss() {
    this.snackBar.dismiss();
  }

  show() {
    this.snackBar.open(this.message, 'Dismiss', {
      duration: this.autoDismiss ? this.dismissTime : undefined,
      panelClass: ['error-banner']
    });
  }
}
