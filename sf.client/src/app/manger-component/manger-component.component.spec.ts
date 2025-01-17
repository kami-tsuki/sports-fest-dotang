import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MangerComponentComponent } from './manger-component.component';

describe('MangerComponentComponent', () => {
  let component: MangerComponentComponent;
  let fixture: ComponentFixture<MangerComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MangerComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MangerComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
