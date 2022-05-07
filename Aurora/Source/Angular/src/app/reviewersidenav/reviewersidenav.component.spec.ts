import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewersidenavComponent } from './reviewersidenav.component';

describe('ReviewersidenavComponent', () => {
  let component: ReviewersidenavComponent;
  let fixture: ComponentFixture<ReviewersidenavComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReviewersidenavComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewersidenavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
