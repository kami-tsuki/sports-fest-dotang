import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassEditAddModalComponent } from './class-edit-add-modal.component';

describe('EditAddModalComponent', () => {
  let component: ClassEditAddModalComponent;
  let fixture: ComponentFixture<ClassEditAddModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ClassEditAddModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ClassEditAddModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
