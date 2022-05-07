import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticlecardhomeComponent } from './articlecardhome.component';

describe('ArticlecardhomeComponent', () => {
  let component: ArticlecardhomeComponent;
  let fixture: ComponentFixture<ArticlecardhomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ArticlecardhomeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticlecardhomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
