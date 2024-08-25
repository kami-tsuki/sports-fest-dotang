import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditAddModalComponent } from './edit-add-modal.component';

describe('EditAddModalComponent', () => {
  let component: EditAddModalComponent;
  let fixture: ComponentFixture<EditAddModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditAddModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditAddModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
