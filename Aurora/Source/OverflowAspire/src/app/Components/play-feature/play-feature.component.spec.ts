import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayFeatureComponent } from './play-feature.component';

describe('PlayFeatureComponent', () => {
  let component: PlayFeatureComponent;
  let fixture: ComponentFixture<PlayFeatureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlayFeatureComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PlayFeatureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
