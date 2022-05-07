import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrendingArticlepageComponent } from './trending-articlepage.component';

describe('TrendingArticlepageComponent', () => {
  let component: TrendingArticlepageComponent;
  let fixture: ComponentFixture<TrendingArticlepageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrendingArticlepageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrendingArticlepageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
