import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import { MatListModule } from '@angular/material/list';
import {ISchool} from '../../../services/api/sf-client';

@Component({
    selector: 'sf-select-school',
    templateUrl: './select-school.component.html',
    styleUrls: ['./select-school.component.css']
})
export class SelectSchoolComponent {
    constructor(
        public dialogRef: MatDialogRef<SelectSchoolComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { schools: ISchool[], userIsManager: boolean }
    ) {
    }

    selectSchool(school: ISchool) {
        if (!this.data.userIsManager || !school.managerId) {
            this.dialogRef.close(school);
        }
    }

    isSelectable(school: ISchool): boolean {
        return !this.data.userIsManager || !school.managerId;
    }

    close() {
        this.dialogRef.close()
    }
}