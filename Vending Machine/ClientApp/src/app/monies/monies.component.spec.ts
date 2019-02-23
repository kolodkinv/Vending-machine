import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MoniesComponent } from './monies.component';

describe('MoniesComponent', () => {
  let component: MoniesComponent;
  let fixture: ComponentFixture<MoniesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MoniesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MoniesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
