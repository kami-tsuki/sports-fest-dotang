import { Component, Input } from '@angular/core';
import {Placement} from "../../../enums/placement.enum";
import {ApiService} from "../../../services/api.service";
import {IResultModelOfLong} from "../../../services/api/sf-client";

@Component({
  selector: 'sf-user-counter',
  templateUrl: './user-counter.component.html',
  styleUrl: './user-counter.component.css'
})
export class UserCounterComponent {
  public count: number = 0;
  @Input() schoolId: string | undefined
  @Input() placement: Placement = Placement.TopLeft;

  constructor(
      private api: ApiService
  ) {
  }

  ngOnInit() {
    this.api.get<IResultModelOfLong>(`/api/v1/user/count`).subscribe(res => {
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
