import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeuModalConteudoComponent } from './meu-modal-conteudo.component';

describe('MeuModalConteudoComponent', () => {
  let component: MeuModalConteudoComponent;
  let fixture: ComponentFixture<MeuModalConteudoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MeuModalConteudoComponent]
    });
    fixture = TestBed.createComponent(MeuModalConteudoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
