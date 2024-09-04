import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateSchoolComponent } from './create-school.component';

describe('CreateSchoolComponent', () => {
  let component: CreateSchoolComponent;
  let fixture: ComponentFixture<CreateSchoolComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateSchoolComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateSchoolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
