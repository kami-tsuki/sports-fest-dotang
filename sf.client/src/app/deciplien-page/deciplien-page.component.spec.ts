import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeciplienPageComponent } from './deciplien-page.component';

describe('DeciplienPageComponent', () => {
  let component: DeciplienPageComponent;
  let fixture: ComponentFixture<DeciplienPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeciplienPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DeciplienPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
