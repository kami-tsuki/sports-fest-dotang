import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassPageComponent } from './class-page.component';

describe('ClassPageComponent', () => {
  let component: ClassPageComponent;
  let fixture: ComponentFixture<ClassPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ClassPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ClassPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
