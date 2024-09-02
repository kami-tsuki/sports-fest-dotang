import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassDeleteModalComponent } from './class-delete-modal.component';

describe('DeleteModalComponent', () => {
  let component: ClassDeleteModalComponent;
  let fixture: ComponentFixture<ClassDeleteModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ClassDeleteModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ClassDeleteModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
