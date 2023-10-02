import { Component } from '@angular/core';
import { LogService } from '../services/log.service';

@Component({
  selector: 'app-log',
  templateUrl: './log.component.html',
  styleUrls: ['./log.component.css']
})
export class LogComponent {
  resultados: any[] = []; 
  constructor(
    private LogService: LogService,
  ) {
    this.LogService.listar().subscribe((data) => {
      this.resultados = data;
    });
  }
}
