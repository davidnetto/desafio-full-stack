import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LocadoraService } from '../services/locadora.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalService } from '../services/modal.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';;
import { MeuModalConteudoComponent } from '../meu-modal-conteudo/meu-modal-conteudo.component';


@Component({
  selector: 'app-locadora',
  templateUrl: './locadora.component.html',
  styleUrls: ['./locadora.component.css']
})
export class LocadoraComponent implements OnInit {
  bsModalRef?: BsModalRef;
  
  locadora!: FormGroup;
  endereco!: FormGroup;
  isEdicao = false; // Indica se está no modo de edição
  locadoraId: number = 0; // ID da locadora em modo de edição
  showModal = true;



  isSelectInvalid: boolean = false;

  constructor(
    private fb: FormBuilder,
    private locadoraService: LocadoraService,
    private router: Router,
    private modalService: BsModalService,

    
    private route: ActivatedRoute
  ) {
    
  }
  
 
  
  ngAfterViewInit(): void {
    // Adicione um ouvinte de evento ao modal quando o componente é carregado
    const modal = document.getElementById('myModal');
    modal?.addEventListener('shown.bs.modal', () => {
      // Obtenha uma referência para o elemento que deseja focar
      const myInput = document.getElementById('myInput');

      // Defina o foco no elemento desejado
      myInput?.focus();
    });
  }

  
  ngOnInit(): void {
    
    this.locadora = this.fb.group({
      nomeFantasia: ['', [Validators.required]],
      razaoSocial: ['', [Validators.required]],
      cnpj:['', [Validators.required, Validators.pattern(/^[0-9]*$/)]],
      email: ['', [Validators.required, Validators.email]],
      telefone: ['', [Validators.required, Validators.pattern(/^[0-9]*$/)]],

      cep: ['', [Validators.required]],
      rua: ['', [Validators.required]],
      numero: ['', [Validators.required, Validators.pattern(/^[0-9]*$/)]],
      bairro: ['', [Validators.required]],
      estado: ['', [Validators.required, Validators.maxLength(2)]],
      cidade: ['', [Validators.required]],

    });
    this.endereco = this.fb.group({

    });

        // Verifique se há um parâmetro 'id' na rota
    // Verifique se há um parâmetro 'id' na rota
    this.route.params.subscribe((params) => {
      if (params['id']) {
        this.isEdicao = true; // Está em modo de edição
        this.locadoraId = +params['id']; // Obtém o ID da locadora
        this.carregarDetalhesLocadora(this.locadoraId);
      }
    });


    
   
  }


  
  openModal() {
    this.bsModalRef = this.modalService.show(MeuModalConteudoComponent); // Substitua SeuModalComponent pelo nome do seu componente de modal
  }

  carregarDetalhesLocadora(id: number) {
    this.locadoraService.obterLocadoraPorId(id).subscribe(
      (resultado) => {
        this.locadora.patchValue({
          id: resultado.id,
          nomeFantasia: resultado.nomeFantasia,
          razaoSocial: resultado.razaoSocial,
          cnpj: resultado.cnpj,
          email: resultado.email,
          telefone: resultado.telefone,
          cep: resultado.cep,
          rua: resultado.rua,
          bairro:resultado.bairro,
          numero:resultado.numero,
          estado:resultado.estado,
          cidade:resultado.cidade
        });

      },
      (error) => {
        console.error('Erro ao obter locadora:', error);
      }
    );
  }

  // carregarDetalhesLocadora(id: number) {
  //   this.locadoraService.obterLocadoraPorId(id).subscribe(
  //     (resultado) => {
  //         this.locadora.patchValue({
  //           id: resultado.id,
  //           nome: resultado.nome,
  //         });
       
  //     },
  //     (error) => {
  //       console.error('Erro ao obter locadora:', error);
  //     }
  //   );
  // }

  listagem(){
    this.router.navigate(['/locadora-lista']);
  }



  onSubmit() {
    // Marca todos os controles como "tocado" para acionar a validação
    this.locadora.markAllAsTouched();
  
   

    // if(this.locadora.value.montadoraId == ""){
    //   this.isSelectInvalid = true;
    // }else{
    //   this.isSelectInvalid = false;
    // }

    if (this.locadora.valid) {
 
      if (this.isEdicao) {
     
          this.locadoraService.atualizarLocadora(this.locadora.value, this.locadoraId).subscribe(
            (resultado) => {
      
              console.log('Locadora atualizada com sucesso:', resultado);
              this.router.navigate(['/locadora-lista']);
            },
            (error) => {
              this.openModal();
              console.error('Erro ao atualizada locadora:', error);
            }
          );
        


      } else {
        
               // Não está em modo de edição, então crie uma nova locadora
        this.locadoraService.adicionarLocadora(this.locadora.value).subscribe(
          (resultado) => {
            // A locadora foi adicionada com sucesso
            console.log('Locadora adicionada com sucesso:', resultado);
            this.router.navigate(['/locadora-lista']);
            // Realize ações necessárias, como atualizar a lista de locadoras ou redirecionar o usuário
          },
          (error) => {
            this.openModal();
            // Lide com erros, se necessário
          }
        );
    
 
      }

      // this.locadora.reset();
      // this.locadora.markAllAsTouched();
    } else {
      console.error('Por favor, preencha o formulário corretamente.');

    }
    
  }


}
