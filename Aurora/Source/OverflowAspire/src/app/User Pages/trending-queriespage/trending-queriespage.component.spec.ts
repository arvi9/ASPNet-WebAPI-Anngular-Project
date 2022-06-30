import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrendingQueriespageComponent } from './trending-queriespage.component';

describe('TrendingQueriespageComponent', () => {
  let component: TrendingQueriespageComponent;
  let fixture: ComponentFixture<TrendingQueriespageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrendingQueriespageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrendingQueriespageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
