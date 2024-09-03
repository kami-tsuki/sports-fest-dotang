import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TutorCounterComponent } from './tutor-counter.component';

describe('TutorCounterComponent', () => {
  let component: TutorCounterComponent;
  let fixture: ComponentFixture<TutorCounterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TutorCounterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TutorCounterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
