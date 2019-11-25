import { TestBed } from '@angular/core/testing';

import { MsalService } from './msal.service';
import { HttpClientModule } from '@angular/common/http';

describe('MsalService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [ HttpClientModule]
  }));

  it('should be created', () => {
    const service: MsalService = TestBed.get(MsalService);
    expect(service).toBeTruthy();
  });
});
