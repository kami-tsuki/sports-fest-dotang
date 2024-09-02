import {Component, Inject} from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import {Class} from '../../../services/api/sf-client';

@Component({
    selector: 'sf-edit-add-modal',
    templateUrl: './class-edit-add-modal.component.html',
    styleUrls: ['./class-edit-add-modal.component.css']
})
export class ClassEditAddModalComponent {
    constructor(
        public dialogRef: MatDialogRef<ClassEditAddModalComponent>,
        @Inject(MAT_DIALOG_DATA) public data: {
            class: Class,
            isEdit: boolean
        }
    ) {
    }

    onCancel(): void {
        this.dialogRef.close();
    }

    onSubmit(): void {
        this.dialogRef.close(this.data.class);
    }
}