import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LocadoraService } from '../../services/locadora.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-locadora',
  templateUrl: './locadora-lista.component.html',
  styleUrls: ['./locadora-lista.component.css']
})
export class LocadoraListaComponent implements OnInit {
  locadoras: any[] = [];

  constructor(
    private fb: FormBuilder,
    private locadoraService: LocadoraService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.carregarLocadoras();
  }

  carregarLocadoras() {
    this.locadoraService.listarLocadoras().subscribe(
      (resultado) => {
        this.locadoras = resultado;
        console.log('Locadoras carregadas com sucesso:', resultado);
      },
      (error) => {
        console.error('Erro ao carregar locadoras:', error);
      }
    );
  }

  atualizarLocadora(locadoraId: any) {
    this.router.navigate([`/locadora/${locadoraId}`]);
    // Implemente a lógica para atualizar a locadora
  }


  deletarLocadora(id: number) {
    this.locadoraService.excluirLocadora(id).subscribe(
      () => {
        console.log('Locadora excluída com sucesso');
      this.recarregarPagina();
        
        // Adicione aqui qualquer lógica adicional após a exclusão
      },
      (error) => {
        console.error('Erro ao excluir locadora:', error);
        // Lide com erros, se necessário
      }
    );
  }

  cadastrarLocadora(){
    this.router.navigate([`/locadora/`]);
  }

  recarregarPagina() {
    // Obtém a rota atual
    const currentRoute = this.router.url;

    // Navega para a mesma rota
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentRoute]);
    });
  }
  onSubmit() {
    // Implemente a lógica para processar o formulário quando necessário
  }
}
