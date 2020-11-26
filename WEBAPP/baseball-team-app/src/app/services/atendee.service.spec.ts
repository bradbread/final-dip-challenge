import { TestBed } from '@angular/core/testing';

import { AtendeeService } from './atendee.service';

describe('AtendeeService', () => {
  let service: AtendeeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AtendeeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
