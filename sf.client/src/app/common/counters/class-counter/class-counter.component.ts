import {Component, Input, OnInit} from '@angular/core';
import {ApiService} from "../../../services/api.service";
import {IResultModelOfLong} from "../../../services/api/sf-client";
import {Placement} from "../../../enums/placement.enum";

@Component({
    selector: 'sf-class-counter',
    templateUrl: './class-counter.component.html',
    styleUrl: './class-counter.component.css'
})
export class ClassCounterComponent implements OnInit {
    public count: number = 0;
    @Input() schoolId: string | undefined
    @Input() placement: Placement = Placement.TopLeft;

    constructor(
        private api: ApiService
    ) {
    }

    ngOnInit() {
        let filters: string[] = [];
        if (this.schoolId) {
            filters.push(`schoolId-eq=${this.schoolId}`);
        }
        let filter = filters.join('&');
        if (filter) {
            filter = `?${filter}`;
        }
        this.api.get<IResultModelOfLong>(`/api/v1/data/class/count${filter}`).subscribe(res => {
            if (res && res.success) {
                this.count = res.data || 0;
            } else if (res && res.error) {
                console.error(res.error);
            } else {
                console.error('Error fetching class count');
            }
        });

    }

}
