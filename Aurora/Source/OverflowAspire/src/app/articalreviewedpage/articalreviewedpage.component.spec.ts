import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticalreviewedpageComponent } from './articalreviewedpage.component';

describe('ArticalreviewedpageComponent', () => {
  let component: ArticalreviewedpageComponent;
  let fixture: ComponentFixture<ArticalreviewedpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ArticalreviewedpageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticalreviewedpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
