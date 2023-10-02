import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LocadorasxveiculosComponent } from './locadorasxveiculos.component';

describe('LocadorasxveiculosComponent', () => {
  let component: LocadorasxveiculosComponent;
  let fixture: ComponentFixture<LocadorasxveiculosComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LocadorasxveiculosComponent]
    });
    fixture = TestBed.createComponent(LocadorasxveiculosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
