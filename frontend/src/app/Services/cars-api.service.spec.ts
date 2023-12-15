import { TestBed } from '@angular/core/testing';

import { CarsAPIService } from './cars-api.service';

describe('CarsAPIService', () => {
  let service: CarsAPIService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CarsAPIService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
