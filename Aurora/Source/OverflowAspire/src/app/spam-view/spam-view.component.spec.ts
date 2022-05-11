import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpamViewComponent } from './spam-view.component';

describe('SpamViewComponent', () => {
  let component: SpamViewComponent;
  let fixture: ComponentFixture<SpamViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SpamViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SpamViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
