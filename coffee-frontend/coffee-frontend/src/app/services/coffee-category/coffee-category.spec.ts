import { TestBed } from '@angular/core/testing';

import { CoffeeCategoryService } from './coffee-category';

describe('CoffeeCategory', () => {
  let service: CoffeeCategoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CoffeeCategoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
