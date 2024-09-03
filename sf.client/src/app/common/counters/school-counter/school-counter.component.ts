import {Component, Input} from '@angular/core';
import {ApiService} from "../../../services/api.service";
import {IResultModelOfLong} from "../../../services/api/sf-client";
import { Placement } from '../../../enums/placement.enum';

@Component({
    selector: 'sf-school-counter',
    templateUrl: './school-counter.component.html',
    styleUrl: './school-counter.component.css'
})
export class SchoolCounterComponent {
    public count: number = 0;
    //@Input() schoolId: string | undefined
    @Input() placement: Placement = Placement.TopLeft;

    constructor(
        private api: ApiService
    ) {
    }

    ngOnInit() {
        // let filters: string[] = [];
        // if (this.schoolId) {
        //     //filters.push(`schoolId-eq=${this.schoolId}`);
        // }
        // let filter = filters.join('&');
        // if (filter) {
        //     filter = `?${filter}`;
        // }
        this.api.get<IResultModelOfLong>(`/api/v1/data/school/count`).subscribe(res => {
            if (res && res.success) {
                this.count = res.data || 0;
            } else if (res && res.error) {
                console.error(res.error);
            } else {
                console.error('Error fetching school count');
            }
        });

    }

    protected readonly Placement = Placement;
}
