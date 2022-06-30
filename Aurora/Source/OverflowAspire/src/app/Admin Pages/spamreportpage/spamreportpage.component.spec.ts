import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpamreportpageComponent } from './spamreportpage.component';

describe('SpamreportpageComponent', () => {
  let component: SpamreportpageComponent;
  let fixture: ComponentFixture<SpamreportpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SpamreportpageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SpamreportpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
