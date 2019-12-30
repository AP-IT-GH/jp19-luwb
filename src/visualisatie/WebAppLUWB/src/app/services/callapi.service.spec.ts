import { TestBed } from '@angular/core/testing';

import { CallapiService } from '../services/callapi.service';
import { HttpClientModule } from '@angular/common/http';

describe('CallapiService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientModule]
  }));

  it('should be created', () => {
    const service: CallapiService = TestBed.get(CallapiService);
    expect(service).toBeTruthy();
  });
});
