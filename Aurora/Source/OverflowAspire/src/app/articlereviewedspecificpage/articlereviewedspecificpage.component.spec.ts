import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticlereviewedspecificpageComponent } from './articlereviewedspecificpage.component';

describe('ArticlereviewedspecificpageComponent', () => {
  let component: ArticlereviewedspecificpageComponent;
  let fixture: ComponentFixture<ArticlereviewedspecificpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ArticlereviewedspecificpageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticlereviewedspecificpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
