import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LocadoraListaComponent } from './locadora-lista.component';

describe('LocadoraListaComponent', () => {
  let component: LocadoraListaComponent;
  let fixture: ComponentFixture<LocadoraListaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LocadoraListaComponent]
    });
    fixture = TestBed.createComponent(LocadoraListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
