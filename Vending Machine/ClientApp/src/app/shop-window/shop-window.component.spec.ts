import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShopWindowComponent } from './shop-window.component';

describe('ShopWindowComponent', () => {
  let component: ShopWindowComponent;
  let fixture: ComponentFixture<ShopWindowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShopWindowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShopWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
