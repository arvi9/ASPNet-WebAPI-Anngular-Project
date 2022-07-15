import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RejectreasonComponent } from './rejectreason.component';

describe('RejectreasonComponent', () => {
  let component: RejectreasonComponent;
  let fixture: ComponentFixture<RejectreasonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RejectreasonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RejectreasonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
