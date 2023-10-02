import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VeiculoService } from '../../services/veiculo.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-veiculo',
  templateUrl: './veiculo-lista.component.html',
  styleUrls: ['./veiculo-lista.component.css']
})
export class VeiculoListaComponent implements OnInit {
  veiculos: any[] = [];

  constructor(
    private fb: FormBuilder,
    private veiculoService: VeiculoService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.carregarVeiculos();
  }

  carregarVeiculos() {
    this.veiculoService.listarVeiculos().subscribe(
      (resultado) => {
        this.veiculos = resultado;
        console.log('Veiculos carregadas com sucesso:', resultado);
      },
      (error) => {
        console.error('Erro ao carregar veiculos:', error);
      }
    );
  }

  atualizarVeiculo(veiculoId: any) {
    this.router.navigate([`/veiculo/${veiculoId}`]);
    // Implemente a lógica para atualizar a veiculo
  }


  deletarVeiculo(id: number) {
    this.veiculoService.excluirVeiculo(id).subscribe(
      () => {
        console.log('Veiculo excluída com sucesso');
      this.recarregarPagina();
        
        // Adicione aqui qualquer lógica adicional após a exclusão
      },
      (error) => {
        console.error('Erro ao excluir veiculo:', error);
        // Lide com erros, se necessário
      }
    );
  }

  cadastrarVeiculo(){
    this.router.navigate([`/veiculo/`]);
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
