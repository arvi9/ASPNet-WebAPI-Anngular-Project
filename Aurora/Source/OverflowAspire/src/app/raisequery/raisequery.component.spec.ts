import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RaisequeryComponent } from './raisequery.component';

describe('RaisequeryComponent', () => {
  let component: RaisequeryComponent;
  let fixture: ComponentFixture<RaisequeryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RaisequeryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RaisequeryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
