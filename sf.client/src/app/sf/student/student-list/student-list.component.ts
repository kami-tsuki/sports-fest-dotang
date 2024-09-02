import {Component, Input, OnInit} from '@angular/core';
import {ApiService} from '../../../services/api.service';
import {Student, ResultModelOfPageOfStudent, IResultModelOfPageOfStudent} from '../../../services/api/sf-client';

@Component({
    selector: 'app-student-list',
    templateUrl: './student-list.component.html',
    styleUrls: ['./student-list.component.css']
})
export class StudentListComponent implements OnInit {
    @Input() classId: string | null = null;
    public students: Student[] = [];
    public page = 1;
    public pageSize = 10;
    public totalEntities = 0;
    public totalPages = 1;

    constructor(private api: ApiService) {
    }

    ngOnInit(): void {
        if (this.classId) {
            this.loadStudents();
        } else {
            console.error('No class ID provided');
        }
    }

    loadStudents(): void {
        this.api.get<IResultModelOfPageOfStudent>(`/api/v1/data/class/${this.classId}/students?page=${this.page}&entities=${this.pageSize}`)
            .subscribe({
                next: (result) => {
                    if (result.data) {
                        this.students = result.data.data || [];
                        this.totalEntities = result.data.total as number;
                        this.totalPages = Math.ceil(this.totalEntities / this.pageSize);
                    } else {
                        console.error('Failed to load students');
                    }
                },
                error: (err) => {
                    console.error('Error loading students:', err);
                }
            });
    }

    nextPage(): void {
        if (this.page < this.totalPages) {
            this.page++;
            this.loadStudents();
        }
    }

    prevPage(): void {
        if (this.page > 1) {
            this.page--;
            this.loadStudents();
        }
    }
}