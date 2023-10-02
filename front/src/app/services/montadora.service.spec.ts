import { TestBed } from '@angular/core/testing';

import { MontadoraService } from './montadora.service';

describe('MontadoraService', () => {
  let service: MontadoraService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MontadoraService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
