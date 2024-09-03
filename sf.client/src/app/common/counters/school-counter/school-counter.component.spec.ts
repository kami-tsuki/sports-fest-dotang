import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SchoolCounterComponent } from './school-counter.component';

describe('SchoolCounterComponent', () => {
  let component: SchoolCounterComponent;
  let fixture: ComponentFixture<SchoolCounterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SchoolCounterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SchoolCounterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
