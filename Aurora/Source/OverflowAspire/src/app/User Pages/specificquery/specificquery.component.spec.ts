import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpecificqueryComponent } from './specificquery.component';

describe('SpecificqueryComponent', () => {
  let component: SpecificqueryComponent;
  let fixture: ComponentFixture<SpecificqueryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SpecificqueryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SpecificqueryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
