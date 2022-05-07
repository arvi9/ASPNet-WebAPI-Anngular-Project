import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LatestArticlepageComponent } from './latest-articlepage.component';

describe('LatestArticlepageComponent', () => {
  let component: LatestArticlepageComponent;
  let fixture: ComponentFixture<LatestArticlepageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LatestArticlepageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LatestArticlepageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
