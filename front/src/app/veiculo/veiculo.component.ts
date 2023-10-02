import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VeiculoService } from '../services/veiculo.service';
import { ModeloService } from '../services/modelo-service';
import { LocadoraService } from '../services/locadora.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalService } from '../services/modal.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { VeiculoListaComponent } from './veiculo-lista/veiculo-lista.component';
import { MeuModalConteudoComponent } from '../meu-modal-conteudo/meu-modal-conteudo.component';
@Component({
  selector: 'app-veiculo',
  templateUrl: './veiculo.component.html',
  styleUrls: ['./veiculo.component.css']
})
export class VeiculoComponent implements OnInit {
  bsModalRef?: BsModalRef;

  veiculo!: FormGroup;
  isEdicao = false; // Indica se está no modo de edição
  veiculoId: number = 0; // ID da veiculo em modo de edição
  showModal = true;

  veiculoResult: any;
  locadoraResult: any[] = []; 

  modeloResult: any[] = []; 

  constructor(
    private fb: FormBuilder,
    private veiculoService: VeiculoService,
    private ModeloService: ModeloService,
    private LocadoraService: LocadoraService,
    private router: Router,
    private modalService: BsModalService,
    private route: ActivatedRoute
  ) {
    this.ModeloService.listarModelos().subscribe((data) => {
      this.modeloResult = data;
    });
    this.LocadoraService.listarLocadoras().subscribe((data) => {
      this.locadoraResult = data;
    });
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
    
    this.veiculo = this.fb.group({
      numeroPortas: ['', [Validators.required, Validators.pattern(/^[0-9]*$/)]],
      cor: ['', [Validators.required]],
      fabricante: ['', [Validators.required]],
      anoModelo: ['', [Validators.required, Validators.pattern(/^[0-9]*$/)]],
      anoFabricacao: ['', [Validators.required, Validators.pattern(/^[0-9]*$/)]],
      placa: ['', [Validators.required]],
      chassi: ['', [Validators.required]],
      modeloId: ['', [Validators.required]],
      locadoraId: ['', [Validators.required]],
    });

        // Verifique se há um parâmetro 'id' na rota
    // Verifique se há um parâmetro 'id' na rota
    this.route.params.subscribe((params) => {
      if (params['id']) {
        this.isEdicao = true; // Está em modo de edição
        this.veiculoId = +params['id']; // Obtém o ID da veiculo
        this.carregarDetalhesVeiculo(this.veiculoId);
      }
    });
  }

  openModal() {
    this.bsModalRef = this.modalService.show(MeuModalConteudoComponent); // Substitua SeuModalComponent pelo nome do seu componente de modal
  }



  carregarDetalhesVeiculo(id: number) {
    this.veiculoService.obterVeiculoPorId(id).subscribe(
      (resultado) => {
          this.veiculo.patchValue({
            id: resultado.id,
            numeroPortas: resultado.numeroPortas,
            cor: resultado.cor,
            fabricante: resultado.fabricante,
            anoModelo: resultado.anoModelo,
            anoFabricacao: resultado.anoFabricacao,
            placa: resultado.placa,
            chassi: resultado.chassi,
            locadoraId: resultado.locadoraId,
            dataCriacao: resultado.dataCriacao,
            modeloId: resultado.modeloId
          });

          this.veiculoResult = resultado;
      },
      (error) => {
        console.error('Erro ao obter veiculo:', error);
      }
    );
  }



  listagem(){
    this.router.navigate(['/veiculo-lista']);
  }
  onSubmit() {
    // Marca todos os controles como "tocado" para acionar a validação
    this.veiculo.markAllAsTouched();

    if (this.veiculo.valid) {
 
      if (this.isEdicao) {
        // Está em modo de edição, então atualize a veiculo
          this.veiculo.value.dataCriacao = this.veiculoResult.dataCriacao;
     
     
          this.veiculoService.atualizarVeiculo(this.veiculo.value, this.veiculoId).subscribe(
            (resultado) => {
      
              console.log('Veiculo atualizada com sucesso:', resultado);
              this.router.navigate(['/veiculo-lista']);
            },
            (error) => {
              this.openModal();
              console.error('Erro ao atualizada veiculo:', error);
            }
          );
        


      } else {
        
               // Não está em modo de edição, então crie uma nova veiculo
        this.veiculoService.adicionarVeiculo(this.veiculo.value).subscribe(
          (resultado) => {
            // A veiculo foi adicionada com sucesso
            console.log('Veiculo adicionada com sucesso:', resultado);
            this.router.navigate(['/veiculo-lista']);
            // Realize ações necessárias, como atualizar a lista de veiculos ou redirecionar o usuário
          },
          (error) => {
            this.openModal();
            // Lide com erros, se necessário
          }
        );
    
 
      }

      // this.veiculo.reset();
      // this.veiculo.markAllAsTouched();
    } else {
      console.error('Por favor, preencha o formulário corretamente.');
    }
    
  }


}
