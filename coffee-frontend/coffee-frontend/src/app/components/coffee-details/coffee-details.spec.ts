import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoffeeDetails } from './coffee-details';

describe('CoffeeDetails', () => {
  let component: CoffeeDetails;
  let fixture: ComponentFixture<CoffeeDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CoffeeDetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CoffeeDetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
