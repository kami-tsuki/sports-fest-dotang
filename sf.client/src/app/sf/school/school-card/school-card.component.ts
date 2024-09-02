import {Component, Input} from '@angular/core';
import {School} from '../../../services/api/sf-client';
import {ApiService} from "../../../services/api.service";

@Component({
    selector: 'sf-school-card',
    templateUrl: './school-card.component.html',
    styleUrls: ['./school-card.component.css']
})
export class SchoolCardComponent {
    @Input() school: School | undefined;

    constructor(
        private api: ApiService
    ) {
    }
}