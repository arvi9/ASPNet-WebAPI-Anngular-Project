import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyqueryspecificComponent } from './myqueryspecific.component';

describe('MyqueryspecificComponent', () => {
  let component: MyqueryspecificComponent;
  let fixture: ComponentFixture<MyqueryspecificComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyqueryspecificComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MyqueryspecificComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
