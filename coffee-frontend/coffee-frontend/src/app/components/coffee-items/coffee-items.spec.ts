import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoffeeItems } from './coffee-items';

describe('CoffeeItems', () => {
  let component: CoffeeItems;
  let fixture: ComponentFixture<CoffeeItems>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CoffeeItems]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CoffeeItems);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
