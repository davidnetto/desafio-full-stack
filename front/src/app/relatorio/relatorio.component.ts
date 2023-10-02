// relatorio.component.ts

import { Component, OnInit } from '@angular/core';
import { VeiculoService } from '../services/veiculo.service';
import { ModeloService } from '../services/modelo-service';

@Component({
  selector: 'app-relatorio',
  templateUrl: './relatorio.component.html',
  styleUrls: ['./relatorio.component.css']
})
export class RelatorioComponent implements OnInit {
  locadoraFilter: string = '';
  modeloFilter: number = 0;
  dataInicioFilter: string = '';
  dataFimFilter: string = '';

  
  modeloResult: any[] = []; 
  // Simule uma lista de modelos
  modelos: any[] = []; 

  // Simule uma lista de resultados
  resultados: any[] = [];

  constructor(
    private VeiculoService: VeiculoService,
    private ModeloService: ModeloService,
  ) {
    this.ModeloService.listarModelos().subscribe((data) => {
      this.modelos = data;
    });
  }

  ngOnInit(): void {
    this.filtrarVeiculos();
  }

  limparFiltros() {
    // Limpar os valores dos filtros para os valores iniciais
    this.locadoraFilter = '';
    this.modeloFilter = 0;
    this.dataInicioFilter = '';
    this.dataFimFilter = '';
  }

  filtrarVeiculos() {
    // Chame o serviço de VeiculoService para obter os veículos filtrados
    this.VeiculoService.filtrarVeiculos(
      this.locadoraFilter,
      this.modeloFilter,
      this.dataInicioFilter,
      this.dataFimFilter,
    ).subscribe(veiculos => {
      this.resultados = veiculos;
    });
  }



}
