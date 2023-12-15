import { TestBed } from '@angular/core/testing';

import { AddCarGuard } from './add-car.guard';

describe('AddCarGuard', () => {
  let guard: AddCarGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(AddCarGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
