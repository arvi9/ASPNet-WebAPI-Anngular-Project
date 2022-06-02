import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewerPiechartComponent } from './reviewer-piechart.component';

describe('ReviewerPiechartComponent', () => {
  let component: ReviewerPiechartComponent;
  let fixture: ComponentFixture<ReviewerPiechartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReviewerPiechartComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewerPiechartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
