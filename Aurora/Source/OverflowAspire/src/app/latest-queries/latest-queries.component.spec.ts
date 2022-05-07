import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LatestQueriesComponent } from './latest-queries.component';

describe('LatestQueriesComponent', () => {
  let component: LatestQueriesComponent;
  let fixture: ComponentFixture<LatestQueriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LatestQueriesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LatestQueriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
