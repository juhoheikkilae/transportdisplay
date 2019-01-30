import { TestBed } from '@angular/core/testing';

import { StopFinderService } from './stop-finder.service';

describe('StopFinderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: StopFinderService = TestBed.get(StopFinderService);
    expect(service).toBeTruthy();
  });
});
