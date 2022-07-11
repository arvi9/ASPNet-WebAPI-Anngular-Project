import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PrivatearticlesComponent } from './privatearticles.component';

describe('PrivatearticlesComponent', () => {
  let component: PrivatearticlesComponent;
  let fixture: ComponentFixture<PrivatearticlesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrivatearticlesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PrivatearticlesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
