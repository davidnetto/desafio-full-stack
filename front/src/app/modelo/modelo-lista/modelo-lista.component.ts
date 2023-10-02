import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModeloService } from '../../services/modelo-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-modelo',
  templateUrl: './modelo-lista.component.html',
  styleUrls: ['./modelo-lista.component.css']
})
export class ModeloListaComponent implements OnInit {
  modelos: any[] = [];

  constructor(
    private fb: FormBuilder,
    private modeloService: ModeloService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.carregarModelos();
  }

  carregarModelos() {
    this.modeloService.listarModelos().subscribe(
      (resultado) => {
        this.modelos = resultado;
        console.log('Modelos carregadas com sucesso:', resultado);
      },
      (error) => {
        console.error('Erro ao carregar modelos:', error);
      }
    );
  }

  atualizarModelo(modeloId: any) {
    this.router.navigate([`/modelo/${modeloId}`]);
    // Implemente a lógica para atualizar a modelo
  }


  deletarModelo(id: number) {
    this.modeloService.excluirModelo(id).subscribe(
      () => {
        console.log('Modelo excluída com sucesso');
      this.recarregarPagina();
        
        // Adicione aqui qualquer lógica adicional após a exclusão
      },
      (error) => {
        console.error('Erro ao excluir modelo:', error);
        // Lide com erros, se necessário
      }
    );
  }

  cadastrarModelo(){
    this.router.navigate([`/modelo/`]);
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
