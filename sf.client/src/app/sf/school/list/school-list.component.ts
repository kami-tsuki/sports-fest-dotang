// File: sf.client/src/app/sf/school/list/school-list.component.ts
import {Component, OnInit} from '@angular/core';
import {ApiService} from '../../../services/api.service';
import {IResultModelOfPageOfSchool, School} from '../../../services/api/sf-client';

@Component({
    selector: 'sf-school-list',
    templateUrl: './school-list.component.html',
    styleUrls: ['./school-list.component.css']
})
export class SchoolListComponent implements OnInit {
    public schools: School[] = [];
    public selected: School | undefined = undefined;
    public columns = 3;

    constructor(private api: ApiService) {
    }

    ngOnInit(): void {
        this.api.get<IResultModelOfPageOfSchool>('/api/v1/data/school')
            .subscribe((result) => {
                if (result.success && result.data) {
                    this.schools = result.data.data || [];
                } else {
                    console.error('Failed to get data');
                }
            });
    }

    selectSchool(school: School) {
        this.selected = school;
    }
}