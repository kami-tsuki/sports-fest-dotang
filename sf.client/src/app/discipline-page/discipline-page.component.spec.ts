import {ComponentFixture, TestBed} from '@angular/core/testing';
import {DisciplinePageComponent} from "@app/discipline-page/discipline-page.component";

describe('DisciplinePageComponent', () => {
    let component: DisciplinePageComponent;
    let fixture: ComponentFixture<DisciplinePageComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [DisciplinePageComponent]
        })
            .compileComponents();

        fixture = TestBed.createComponent(DisciplinePageComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
