import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserverificationpageComponent } from './userverificationpage.component';

describe('UserverificationpageComponent', () => {
  let component: UserverificationpageComponent;
  let fixture: ComponentFixture<UserverificationpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserverificationpageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserverificationpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
