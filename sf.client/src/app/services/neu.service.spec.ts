import { TestBed } from '@angular/core/testing';

import { NeuService } from './neu.service';

describe('NeuService', () => {
  let service: NeuService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NeuService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
