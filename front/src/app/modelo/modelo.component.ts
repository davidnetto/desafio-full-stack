import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModeloService } from '../services/modelo-service';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalService } from '../services/modal.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';;
import { MeuModalConteudoComponent } from '../meu-modal-conteudo/meu-modal-conteudo.component';
import { MontadoraService } from '../services/montadora.service';

@Component({
  selector: 'app-modelo',
  templateUrl: './modelo.component.html',
  styleUrls: ['./modelo.component.css']
})
export class ModeloComponent implements OnInit {
  bsModalRef?: BsModalRef;
  
  modelo!: FormGroup;
  isEdicao = false; 
  modeloId: number = 0; 
  showModal = true;

  montadorasResult: any[] = []; 
  selectedMontadoraId: number | null = null; 

  isSelectInvalid: boolean = false;

  constructor(
    private fb: FormBuilder,
    private modeloService: ModeloService,
    private router: Router,
    private modalService: BsModalService,
    private montadoraService: MontadoraService,
    
    private route: ActivatedRoute
  ) {
    this.montadoraService.listarMontadoras().subscribe((data) => {
      this.montadorasResult = data;
    });
  }
  
 
  
  ngAfterViewInit(): void {
    const modal = document.getElementById('myModal');
    modal?.addEventListener('shown.bs.modal', () => {
      const myInput = document.getElementById('myInput');

      myInput?.focus();
    });
  }

  
  ngOnInit(): void {
    
    this.modelo = this.fb.group({
      nome: ['', [Validators.required]],
      montadoraId: ['', Validators.required]
    });

    this.route.params.subscribe((params) => {
      if (params['id']) {
        this.isEdicao = true; 
        this.modeloId = +params['id'];
        this.carregarDetalhesModelo(this.modeloId);
      }
    });


    
   
  }


  
  openModal() {
    this.bsModalRef = this.modalService.show(MeuModalConteudoComponent); 
  }

  carregarDetalhesModelo(id: number) {
    this.modeloService.obterModeloPorId(id).subscribe(
      (resultado) => {
        this.modelo.patchValue({
          id: resultado.id,
          nome: resultado.nome,
          montadoraId: resultado.montadoraId, 
        });
      },
      (error) => {
        console.error('Erro ao obter modelo:', error);
      }
    );
  }

  listagem(){
    this.router.navigate(['/modelo-lista']);
  }


  onSubmit() {
  
    this.modelo.markAllAsTouched();
  
    if(this.modelo.value.montadoraId == ""){
      this.isSelectInvalid = true;
    }else{
      this.isSelectInvalid = false;
    }

    if (this.modelo.valid) {
 
      if (this.isEdicao) {  
          this.modeloService.atualizarModelo(this.modelo.value, this.modeloId).subscribe(
            (resultado) => {
      
              console.log('Modelo atualizada com sucesso:', resultado);
              this.router.navigate(['/modelo-lista']);
            },
            (error) => {
              this.openModal();
              console.error('Erro ao atualizada modelo:', error);
            }
          );
        


      } else {
              
        this.modeloService.adicionarModelo(this.modelo.value).subscribe(
          (resultado) => {
            
            console.log('Modelo adicionada com sucesso:', resultado);
            this.router.navigate(['/modelo-lista']);
           
          },
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
