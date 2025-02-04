import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TopClassComponent } from './top-class.component';

describe('TopClassComponent', () => {
  let component: TopClassComponent;
  let fixture: ComponentFixture<TopClassComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TopClassComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TopClassComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
