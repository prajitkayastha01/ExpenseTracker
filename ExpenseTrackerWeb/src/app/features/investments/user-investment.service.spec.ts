import { TestBed } from '@angular/core/testing';

import { UserInvestmentService } from './user-investment.service';

describe('UserInvestmentService', () => {
  let service: UserInvestmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserInvestmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
