import { TestBed } from '@angular/core/testing';

import { CallapiService } from './callapi.service';

describe('CallapiService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CallapiService = TestBed.get(CallapiService);
    expect(service).toBeTruthy();
  });
});
