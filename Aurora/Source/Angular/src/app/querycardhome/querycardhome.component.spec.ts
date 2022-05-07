import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuerycardhomeComponent } from './querycardhome.component';

describe('QuerycardhomeComponent', () => {
  let component: QuerycardhomeComponent;
  let fixture: ComponentFixture<QuerycardhomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuerycardhomeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(QuerycardhomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
