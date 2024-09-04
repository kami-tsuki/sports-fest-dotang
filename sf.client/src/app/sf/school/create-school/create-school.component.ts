import {Component, Inject, Input, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {MatListModule} from '@angular/material/list';
import {ApiService} from '../../../services/api.service';
import {IResultModelOfSchool, IResultModelOfUser, ISchool, IUser, RoleType} from '../../../services/api/sf-client';

@Component({
    selector: 'sf-create-school',
    templateUrl: './create-school.component.html',
    styleUrls: ['./create-school.component.css']
})
export class CreateSchoolComponent implements OnInit {
    public schoolName: string = '';
    @Input() public managerId: string | undefined;

    public short: string | undefined = '';
    private user: IUser | undefined;

    constructor(
        public dialogRef: MatDialogRef<CreateSchoolComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { managerId: string | undefined },
        private api: ApiService
    ) {
        this.managerId = data.managerId;
        console.log(`${this.managerId}` || 'No manager id');
    }

    ngOnInit(): void {
        if (this.managerId) {
            this.api.get<IResultModelOfUser>(`/api/v1/user/manager/${this.managerId}`).subscribe(data => {
                if (data && data.success && data.data) {


                    this.user = data.data || undefined;

                    if (this.user?.role === RoleType.CampaignManager) {
                        this.schoolName = `${this.user.first}'s School` || '';
                        this.managerId = this.user.id || '';
                    } else {
                        console.warn('User is not a campaign manager');
                    }

                } else {
                    console.warn('Failed to load user');
                }

            });
        }
    }

    createSchool() {
        const newSchool: ISchool = {
            name: this.schoolName,
            managerId: this.managerId,
            shortName: this.short,
            created: new Date(),
            updated: new Date()
        };
        this.api.post<IResultModelOfSchool>('/api/v1/data/school', newSchool).subscribe(result => {
            if (!result.success) {
                console.warn('Failed to create school');
                return;
            }
            this.dialogRef.close(result);
        });
    }

    close() {
        this.dialogRef.close();
    }
}