import { TestBed } from '@angular/core/testing';

import { DisciplineService } from './discipline.service';

describe('DisciplineService', () => {
  let service: DisciplineService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DisciplineService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
