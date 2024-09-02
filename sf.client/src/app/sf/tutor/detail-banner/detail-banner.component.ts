import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {ResultModelOfTutor, Tutor} from "../../../services/api/sf-client";
import {ApiService} from "../../../services/api.service";

@Component({
    selector: 'sf-tutor-detail-banner',
    templateUrl: './detail-banner.component.html',
    styleUrl: './detail-banner.component.css'
})
export class DetailBannerComponent implements OnInit {

    constructor(
        private HttpClient: HttpClient,
        private api: ApiService
        ) {
    }
    public tutor: Tutor | undefined = undefined;
    @Input() classId!: string | undefined;

    ngOnInit(): void {
        if (this.classId) {
            this.loadTutorDetail(this.classId);
        } else {
            console.error('No class ID provided');
        }
    }


    unassignTutor(id: string | undefined, tutorId: string | undefined) {

    }

    assignTutor(id: string | undefined) {

    }

    createTutor(id: string | undefined) {

    }

    loadTutorDetail(classId: string): void {
        this.api.get<ResultModelOfTutor>(`/api/v1/data/class/${classId}/tutor`)
            .subscribe({
                next: (result) => {
                    if (result && result.success) {
                        this.tutor = result.data;
                    } else {
                        console.error('Invalid tutor ID');
                    }
                },
                error: (err) => {
                    console.error('Error loading tutor detail:', err);
                }
            });
    }
}
