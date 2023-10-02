import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MontadoraService } from '../services/montadora.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalService } from '../services/modal.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { MontadoraListaComponent } from './montadora-lista/montadora-lista.component';
import { MeuModalConteudoComponent } from '../meu-modal-conteudo/meu-modal-conteudo.component';
@Component({
  selector: 'app-montadora',
  templateUrl: './montadora.component.html',
  styleUrls: ['./montadora.component.css']
})
export class MontadoraComponent implements OnInit {
  bsModalRef?: BsModalRef;

  montadora!: FormGroup;
  isEdicao = false; 
  montadoraId: number = 0; 
  showModal = true;

  constructor(
    private fb: FormBuilder,
    private montadoraService: MontadoraService,
    private router: Router,
    private modalService: BsModalService,
    private route: ActivatedRoute
  ) {
    // ...
  }
  
  ngAfterViewInit(): void {
    const modal = document.getElementById('myModal');
    modal?.addEventListener('shown.bs.modal', () => {
      const myInput = document.getElementById('myInput');


      myInput?.focus();
    });
  }

  ngOnInit(): void {
    
    this.montadora = this.fb.group({
      nome: ['', [Validators.required]]
    });

    this.route.params.subscribe((params) => {
      if (params['id']) {
        this.isEdicao = true;
        this.montadoraId = +params['id'];
        this.carregarDetalhesMontadora(this.montadoraId);
      }
    });
  }

  openModal() {
    this.bsModalRef = this.modalService.show(MeuModalConteudoComponent); // Substitua SeuModalComponent pelo nome do seu componente de modal
  }

  carregarDetalhesMontadora(id: number) {
    this.montadoraService.obterMontadoraPorId(id).subscribe(
      (resultado) => {
          this.montadora.patchValue({
            id: resultado.id,
            nome: resultado.nome,
          });
      },
      (error) => {
        console.error('Erro ao obter montadora:', error);
      }
    );
  }

  listagem(){
    this.router.navigate(['/montadora-lista']);
  }
  onSubmit() {
    this.montadora.markAllAsTouched();
    if (this.montadora.valid) {
      if (this.isEdicao) {
          this.montadoraService.atualizarMontadora(this.montadora.value, this.montadoraId).subscribe(
            (resultado) => {
      
              console.log('Montadora atualizada com sucesso:', resultado);
              this.router.navigate(['/montadora-lista']);
            },
            (error) => {
              this.openModal();
              console.error('Erro ao atualizada montadora:', error);
            }
          );
      } else {
        this.montadoraService.adicionarMontadora(this.montadora.value).subscribe(
          (resultado) => {
            console.log('Montadora adicionada com sucesso:', resultado);
            this.router.navigate(['/montadora-lista']);      },
          (error) => {
            this.openModal();
          }
        );
      }
    } else {
      console.error('Por favor, preencha o formulário corretamente.');
    } 
  }
}
