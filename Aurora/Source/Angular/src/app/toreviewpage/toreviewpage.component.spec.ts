import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ToreviewpageComponent } from './toreviewpage.component';

describe('ToreviewpageComponent', () => {
  let component: ToreviewpageComponent;
  let fixture: ComponentFixture<ToreviewpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ToreviewpageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ToreviewpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
