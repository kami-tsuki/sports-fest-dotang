import {ComponentFixture, TestBed} from '@angular/core/testing';

import {HomeCoverComponent} from './home-cover.component';

describe('HomeCoverComponent', () => {
    let component: HomeCoverComponent;
    let fixture: ComponentFixture<HomeCoverComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [HomeCoverComponent]
        })
            .compileComponents();

        fixture = TestBed.createComponent(HomeCoverComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
