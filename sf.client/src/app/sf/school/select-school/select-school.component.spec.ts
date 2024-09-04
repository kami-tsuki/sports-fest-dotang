import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectSchoolComponent } from './select-school.component';

describe('SelectSchoolComponent', () => {
  let component: SelectSchoolComponent;
  let fixture: ComponentFixture<SelectSchoolComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SelectSchoolComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SelectSchoolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
