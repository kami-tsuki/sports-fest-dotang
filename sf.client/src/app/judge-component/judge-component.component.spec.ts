import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JudgeComponentComponent } from './judge-component.component';

describe('JudgeComponentComponent', () => {
  let component: JudgeComponentComponent;
  let fixture: ComponentFixture<JudgeComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [JudgeComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(JudgeComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
