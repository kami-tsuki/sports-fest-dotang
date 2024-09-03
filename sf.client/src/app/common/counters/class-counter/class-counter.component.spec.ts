import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassCounterComponent } from './class-counter.component';

describe('ClassCounterComponent', () => {
  let component: ClassCounterComponent;
  let fixture: ComponentFixture<ClassCounterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ClassCounterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ClassCounterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
