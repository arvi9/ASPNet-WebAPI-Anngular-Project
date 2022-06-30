import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateArticlePageComponent } from './update-article-page.component';

describe('UpdateArticlePageComponent', () => {
  let component: UpdateArticlePageComponent;
  let fixture: ComponentFixture<UpdateArticlePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateArticlePageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateArticlePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
