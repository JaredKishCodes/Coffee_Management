import { TestBed } from '@angular/core/testing';

import { CoffeeCategory } from './coffee-category';

describe('CoffeeCategory', () => {
  let service: CoffeeCategory;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CoffeeCategory);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
