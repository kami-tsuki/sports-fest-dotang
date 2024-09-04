import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {
    IClass,
    IResultModelOfClass, IResultModelOfPageOfSchool,
    IResultModelOfSchool,
    IResultModelOfUser,
    ISchool,
    IUser, RoleType,
    School,
    User
} from "../../services/api/sf-client";
import {ApiService} from "../../services/api.service";
import {Placement} from "../../enums/placement.enum";
import {MatDialog} from '@angular/material/dialog';
import {SelectUserComponent} from "../../sf/user/select-user/select-user.component";
import {SelectSchoolComponent} from "../../sf/school/select-school/select-school.component";
import {CreateSchoolComponent} from "../../sf/school/create-school/create-school.component";

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    constructor(
        private api: ApiService,
        private dialog: MatDialog,
        private route: ActivatedRoute,
        private router: Router
    ) {
    }

    public user: IUser | undefined = undefined;
    public userIsStudent: boolean = false;
    public userIsTutor: boolean = false;
    public userIsManager: boolean = false;
    public userIsJudge: boolean = false;
    public userIsUser: boolean = false;
    public class: IClass | undefined = undefined;
    public classHasTutor: boolean = false;
    public selectedSchool: ISchool | undefined = undefined;
    public schools: ISchool[] = [];

    ngOnInit() {
        this.route.queryParams.subscribe(params => {
            const userId = params['user'];
            const schoolId = params['school'];
            if (userId) {
                this.loadUser(userId);
            }
            if (schoolId) {
                this.loadSchool(schoolId);
            }
        });
    }

    loadUser(id: string) {
        this.api.get<IResultModelOfUser>(`/api/v1/user/${id}`).subscribe(data => {
            this.user = data.data;
            this.updateUserRoles();
            this.selectedSchool = this.loadSchoolByUser(this.user?.id);
            this.class = this.loadClass(this.user?.id);
        });
    }

    loadSchool(id: string) {
        this.api.get<IResultModelOfSchool>(`/api/v1/school/${id}`).subscribe(data => {
            this.selectedSchool = data.data;
        });
    }

    updateUserRoles() {
        this.userIsStudent = !!this.user && this.user.role == RoleType.Student;
        this.userIsTutor = !!this.user && this.user.role == RoleType.Tutor;
        this.userIsManager = !!this.user && this.user.role === RoleType.CampaignManager;
        this.userIsJudge = !!this.user && this.user.role == RoleType.CampaignJudge;
        this.userIsUser = !!this.user && this.user.role == RoleType.User;
    }

    loadSchoolByUser(id: string | undefined): ISchool | undefined {
        if (!id) {
            return undefined;
        }
        if (!this.userIsStudent && !this.userIsTutor && !this.userIsManager && !this.userIsJudge) {
            return undefined;
        }
        this.api.get<IResultModelOfSchool>(this.userIsManager ? `/api/v1/data/school/manager/${id}` :
            this.userIsJudge ? `/api/v1/data/school/judge/${id}` :
                this.userIsTutor ? `/api/v1/data/school/tutor/${id}` :
                    this.userIsStudent ? `/api/v1/data/school/student/${id}` :
                        `/api/v1/data/school/user/${id}`).subscribe(
            (result) => {
                if (result && result.success && result.data) {
                    this.selectedSchool = result.data;
                    return result.data;
                } else {
                    console.warn('Failed to load school', result);
                    return undefined;
                }
            }
        );
        return undefined;
    }

    private loadClass(id: string | undefined): IClass | undefined {
        if (!id && !this.user?.role) {
            console.warn('No user id');
            return undefined;
        }
        if (!this.userIsStudent && !this.userIsTutor) {
            console.warn('User is not a student or tutor');
            return undefined;
        }

        this.api.get<IResultModelOfClass>(`/api/v1/data/class/${this.user?.role?.toLowerCase()}/${id}`).subscribe(
            (result) => {
                if (result.success) {
                    this.class = result.data;
                    return result.data;
                } else {
                    console.warn('Failed to load class');
                    return undefined;
                }
            }
        );
        return undefined;
    }

    openSelectUserModal() {
        const dialogRef = this.dialog.open(SelectUserComponent, {
            width: '400px',
            data: {user: this.user}
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                this.user = result;
                this.updateUserRoles();
                this.selectedSchool = this.loadSchoolByUser(this.user?.id);
                this.class = this.loadClass(this.user?.id);

                let queryParams = {};
                if (this.selectedSchool?.id) {
                    queryParams = {school: this.selectedSchool?.id};
                }
                if (this.user?.id) {
                    queryParams = {user: this.user?.id, ...queryParams};
                }
                this.router.navigate([], {
                    queryParams: queryParams,
                    queryParamsHandling: 'merge'
                });
                console.log(`(${this.userIsManager})(${!this.selectedSchool}) User selected: `, result);
            }
        });
    }

    openSelectSchoolModal() {
        this.api.get<IResultModelOfPageOfSchool>('/api/v1/data/school').subscribe(result => {
            if (result.success && result.data?.data) {
                this.schools = result.data.data;
                const dialogRef = this.dialog.open(SelectSchoolComponent, {
                    width: '400px',
                    data: {schools: this.schools, userIsManager: this.userIsManager}
                });

                dialogRef.afterClosed().subscribe(selectedSchool => {
                    if (selectedSchool) {
                        this.selectedSchool = selectedSchool;
                        this.router.navigate([], {
                            queryParams: {user: this.user?.id, school: this.selectedSchool?.id},
                            queryParamsHandling: 'merge'
                        });
                        console.log('School selected: ', selectedSchool);
                    }
                });
            } else {
                console.warn('Failed to load schools');
            }
        });
    }

    openCreateSchoolModal() {
        console.log('Creating school for user: ', this.user);
        const dialogRef = this.dialog.open(CreateSchoolComponent, {
            width: '400px', data: {managerId: this.user?.id}
        });

        dialogRef.afterClosed().subscribe(newSchool => {
            if (newSchool) {
                this.selectedSchool = newSchool;
                this.router.navigate([], {
                    queryParams: {user: this.user?.id, school: this.selectedSchool?.id},
                    queryParamsHandling: 'merge'
                });
                console.log('School created: ', newSchool);
            }
        });
    }

    protected readonly Placement = Placement;
}