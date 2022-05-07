import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewerdashboardComponent } from './reviewerdashboard.component';

describe('ReviewerdashboardComponent', () => {
  let component: ReviewerdashboardComponent;
  let fixture: ComponentFixture<ReviewerdashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReviewerdashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewerdashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
