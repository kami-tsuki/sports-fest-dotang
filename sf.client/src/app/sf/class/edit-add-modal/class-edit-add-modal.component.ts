import {Component, Inject} from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import {Class, IResultModelOfPageOfSchool, ISchool} from '../../../services/api/sf-client';
import {ApiService} from "../../../services/api.service";

@Component({
    selector: 'sf-edit-add-modal',
    templateUrl: './class-edit-add-modal.component.html',
    styleUrls: ['./class-edit-add-modal.component.css']
})
export class ClassEditAddModalComponent {
    public schools: ISchool[] = [];

    constructor(
        private api: ApiService,
        public dialogRef: MatDialogRef<ClassEditAddModalComponent>,
        @Inject(MAT_DIALOG_DATA) public data: {
            class: Class,
            isEdit: boolean
        }
    ) {
        this.api.get<IResultModelOfPageOfSchool>('/api/v1/data/school').subscribe(data => {
            if (data && data.success && data.data) {
                this.schools = data.data?.data || [];
            } else {
                console.warn('Failed to load schools');
            }
        });
    }

    onCancel(): void {
        this.dialogRef.close();
    }

    onSubmit(): void {
        this.dialogRef.close(this.data.class);
    }
}