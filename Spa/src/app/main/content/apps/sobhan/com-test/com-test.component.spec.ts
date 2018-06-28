import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ComTestComponent } from './com-test.component';

describe('ComTestComponent', () => {
  let component: ComTestComponent;
  let fixture: ComponentFixture<ComTestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ComTestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ComTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
