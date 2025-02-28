import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagerComponentComponent } from './manager-component.component';

describe('MangerComponentComponent', () => {
  let component: ManagerComponentComponent;
  let fixture: ComponentFixture<ManagerComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ManagerComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ManagerComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
