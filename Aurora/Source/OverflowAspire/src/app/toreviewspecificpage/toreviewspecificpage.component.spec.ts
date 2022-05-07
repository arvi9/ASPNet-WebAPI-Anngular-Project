import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ToreviewspecificpageComponent } from './toreviewspecificpage.component';

describe('ToreviewspecificpageComponent', () => {
  let component: ToreviewspecificpageComponent;
  let fixture: ComponentFixture<ToreviewspecificpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ToreviewspecificpageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ToreviewspecificpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
