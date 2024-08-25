import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Class } from '../../../services/api/sf-client';

@Component({
  selector: 'sf-edit-add-modal',
  templateUrl: './edit-add-modal.component.html',
  styleUrls: ['./edit-add-modal.component.css']
})
export class EditAddModalComponent {
  constructor(
      public dialogRef: MatDialogRef<EditAddModalComponent>,
      @Inject(MAT_DIALOG_DATA) public data: { class: Class, isEdit: boolean }
  ) {}

  onCancel(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    this.dialogRef.close(this.data.class);
  }
}