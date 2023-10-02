import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MontadoraComponent } from './montadora/montadora.component';
import { MontadoraListaComponent } from './montadora/montadora-lista/montadora-lista.component';
import { ModeloComponent } from './modelo/modelo.component';
import { ModeloListaComponent } from './modelo/modelo-lista/modelo-lista.component';
import { LocadoraComponent } from './locadora/locadora.component';
import { LocadoraListaComponent } from './locadora/locadora-lista/locadora-lista.component';
import { VeiculoComponent } from './veiculo/veiculo.component';
import { VeiculoListaComponent } from './veiculo/veiculo-lista/veiculo-lista.component';
import { RelatorioComponent } from './relatorio/relatorio.component';
import { LocadorasxveiculosComponent } from './locadorasxveiculos/locadorasxveiculos.component';
import { LogComponent } from './log/log.component';

const routes: Routes = [
  { path: '', redirectTo: '/relatorio', pathMatch: 'full' }, // Rota padrão
  { path: 'montadora', component: MontadoraComponent },
  { path: 'montadora/:id', component: MontadoraComponent },
  { path: 'montadora-lista', component: MontadoraListaComponent },
  { path: 'modelo', component: ModeloComponent },
  { path: 'modelo/:id', component: ModeloComponent },
  { path: 'modelo-lista', component: ModeloListaComponent },
  { path: 'locadora', component: LocadoraComponent },
  { path: 'locadora/:id', component: LocadoraComponent },
  { path: 'locadora-lista', component: LocadoraListaComponent },
  { path: 'veiculo', component: VeiculoComponent },
  { path: 'veiculo/:id', component: VeiculoComponent },
  { path: 'veiculo-lista', component: VeiculoListaComponent },
  { path: 'relatorio', component: RelatorioComponent },
  { path: 'locadorasxveiculos', component: LocadorasxveiculosComponent },
  { path: 'log', component: LogComponent },
  // Outras rotas, se necessário
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
