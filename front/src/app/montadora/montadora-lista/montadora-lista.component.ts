import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MontadoraService } from '../../services/montadora.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-montadora',
  templateUrl: './montadora-lista.component.html',
  styleUrls: ['./montadora-lista.component.css']
})
export class MontadoraListaComponent implements OnInit {
  montadoras: any[] = [];

  constructor(
    private fb: FormBuilder,
    private montadoraService: MontadoraService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.carregarMontadoras();
  }

  carregarMontadoras() {
    this.montadoraService.listarMontadoras().subscribe(
      (resultado) => {
        this.montadoras = resultado;
        console.log('Montadoras carregadas com sucesso:', resultado);
      },
      (error) => {
        console.error('Erro ao carregar montadoras:', error);
      }
    );
  }

  atualizarMontadora(montadoraId: any) {
    this.router.navigate([`/montadora/${montadoraId}`]);
    // Implemente a lógica para atualizar a montadora
  }


  deletarMontadora(id: number) {
    this.montadoraService.excluirMontadora(id).subscribe(
      () => {
        console.log('Montadora excluída com sucesso');
      this.recarregarPagina();
        
        // Adicione aqui qualquer lógica adicional após a exclusão
      },
      (error) => {
        console.error('Erro ao excluir montadora:', error);
        // Lide com erros, se necessário
      }
    );
  }

  cadastrarMontadora(){
    this.router.navigate([`/montadora/`]);
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
