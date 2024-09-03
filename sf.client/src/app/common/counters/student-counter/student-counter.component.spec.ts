import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentCounterComponent } from './student-counter.component';

describe('StudentCounterComponent', () => {
  let component: StudentCounterComponent;
  let fixture: ComponentFixture<StudentCounterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StudentCounterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(StudentCounterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
