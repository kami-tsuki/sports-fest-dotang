// delete-modal.component.ts
import {Component, Inject} from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
    selector: 'sf-delete-modal',
    templateUrl: './class-delete-modal.component.html',
    styleUrls: ['./class-delete-modal.component.css']
})
export class ClassDeleteModalComponent<T> {
    constructor(
        public dialogRef: MatDialogRef<ClassDeleteModalComponent<T>>,
        @Inject(MAT_DIALOG_DATA) public data: T
    ) {
    }

    onCancel(): void {
        this.dialogRef.close();
    }

    onConfirm(): void {
        this.dialogRef.close(true);
    }
}