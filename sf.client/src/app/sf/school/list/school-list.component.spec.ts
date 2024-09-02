import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SchoolListComponent } from './school-list.component';

describe('ListComponent', () => {
  let component: SchoolListComponent;
  let fixture: ComponentFixture<SchoolListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SchoolListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SchoolListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
