import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {ApiService} from '../../../services/api.service';
import {
    Class,
    IResultModelOfClass,
    ResultModelOfClass,
    ResultModelOfTutor,
    Student,
    Tutor,
    User
} from '../../../services/api/sf-client';
import {AppPaths} from "../../../app-paths";

@Component({
    selector: 'sf-class-detail',
    templateUrl: './class-detail.component.html',
    styleUrls: ['./class-detail.component.css']
})
export class ClassDetailComponent implements OnInit {
    public classDetail: Class | undefined = undefined;
    public tutor: Tutor | undefined = undefined;
    public students: Student[] = [];
    public selectedTab: 'details' | 'students' = 'details';

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private api: ApiService
    ) {
    }

    ngOnInit(): void {
        const classId = this.route.snapshot.paramMap.get('id');
        if (classId) {
            this.loadClassDetail(classId);
        } else {
            console.error('No class ID provided');
            this.goBackToList();
        }
    }

    loadClassDetail(id: string): void {
        this.api.get<IResultModelOfClass>(`/api/v1/data/class/${id}`)
            .subscribe({
                next: (result) => {
                    if (result && result.success) {
                        this.classDetail = result.data;
                    } else {
                        console.error('Invalid class ID');
                        this.goBackToList();
                    }
                },
                error: (err) => {
                    console.error('Error loading class detail:', err);
                    this.goBackToList();
                }
            });
    }



    goBack() {
        window.history.back();
    }

    goBackToList() {
        this.router.navigate([AppPaths.classBase]).then(r =>
            console.error('Failed to navigate back to class list:', r));
    }

    selectTab(tab: 'details' | 'students') {
        this.selectedTab = tab;
    }


}