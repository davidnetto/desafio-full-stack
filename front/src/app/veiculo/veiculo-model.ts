import { inject } from "@angular/core";

// montadora.model.ts

export interface Veiculo {
    dataCriacao: string;
    chassi: any;
    placa: any;
    anoFabricacao: any;
    anoModelo: any;
    fabricante: any;
    cor: any;
    numeroPortas: any;
    id: number;
    locadoraId: number;
    modeloId: number;
    nome: string;
    // Outras propriedades da montadora, se houverem
  }
  