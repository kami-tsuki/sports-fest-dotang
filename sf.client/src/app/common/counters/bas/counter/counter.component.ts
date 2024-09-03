import { EventEmitter, Input, Output } from "@angular/core";
import { Component } from '@angular/core';
import { Placement } from "../../../../enums/placement.enum";

@Component({
    selector: 'sf-counter',
    templateUrl: './counter.component.html',
    styleUrls: ['./counter.component.css']
})
export class CounterComponent {
    @Input() count: number = 0;
    @Input() label: string = 'Counter';
    @Input() placement: Placement = Placement.TopLeft;
    @Input() showLabel: boolean = true;

    formatCount(count: number): string {
        return count.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    }

    protected readonly Placement = Placement;
}

