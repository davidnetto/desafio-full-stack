import { Component } from '@angular/core';
import { LocadoraService } from '../services/locadora.service';
@Component({
  selector: 'app-locadorasxveiculos',
  templateUrl: './locadorasxveiculos.component.html',
  styleUrls: ['./locadorasxveiculos.component.css']
})
export class LocadorasxveiculosComponent {
  resultados: any[] = []; 
  constructor(
    private LocadoraService: LocadoraService,
  ) {
    this.LocadoraService.listarLocadorasRelatorio().subscribe((data) => {
      this.resultados = data;
    });
  }


}
