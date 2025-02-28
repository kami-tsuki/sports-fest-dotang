import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SportsFestivalComponent } from './sports-festival.component';

describe('SportsFistivalComponent', () => {
  let component: SportsFestivalComponent;
  let fixture: ComponentFixture<SportsFestivalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SportsFestivalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SportsFestivalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
