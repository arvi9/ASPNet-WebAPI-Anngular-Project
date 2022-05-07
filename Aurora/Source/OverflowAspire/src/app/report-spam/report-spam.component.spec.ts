import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportSpamComponent } from './report-spam.component';

describe('ReportSpamComponent', () => {
  let component: ReportSpamComponent;
  let fixture: ComponentFixture<ReportSpamComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReportSpamComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportSpamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
