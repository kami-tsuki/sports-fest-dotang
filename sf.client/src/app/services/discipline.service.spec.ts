import { TestBed } from '@angular/core/testing';

import { DisciplinesService } from './discipline.service';

describe('DisciplineService', () => {
  let service: DisciplinesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DisciplinesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
