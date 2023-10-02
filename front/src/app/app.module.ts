import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { MontadoraComponent } from './montadora/montadora.component';
import { MontadoraListaComponent } from './montadora/montadora-lista/montadora-lista.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MeuModalConteudoComponent } from './meu-modal-conteudo/meu-modal-conteudo.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ModeloComponent } from './modelo/modelo.component';
import { ModeloListaComponent } from './modelo/modelo-lista/modelo-lista.component';
import { FormsModule } from '@angular/forms';
import { LocadoraComponent } from './locadora/locadora.component';
import { LocadoraListaComponent } from './locadora/locadora-lista/locadora-lista.component';
import { VeiculoComponent } from './veiculo/veiculo.component';
import { VeiculoListaComponent } from './veiculo/veiculo-lista/veiculo-lista.component';
import { NumericDirective } from './numeric.directive';
import { RelatorioComponent } from './relatorio/relatorio.component';
import { LocadorasxveiculosComponent } from './locadorasxveiculos/locadorasxveiculos.component';
import { LogComponent } from './log/log.component'; // Importe a diretiva

@NgModule({
  declarations: [
    AppComponent,
    MontadoraComponent,
    MontadoraListaComponent,
    MeuModalConteudoComponent,
    ModeloComponent,
    ModeloListaComponent,
    LocadoraComponent,
    LocadoraListaComponent,
    VeiculoComponent,
    VeiculoListaComponent,
    NumericDirective,
    RelatorioComponent,
    LocadorasxveiculosComponent,
    LogComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,ModalModule.forRoot(),
    FormsModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
