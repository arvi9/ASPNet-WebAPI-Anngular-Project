import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpecificarticleComponent } from './specificarticle.component';

describe('SpecificarticleComponent', () => {
  let component: SpecificarticleComponent;
  let fixture: ComponentFixture<SpecificarticleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SpecificarticleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SpecificarticleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
