import { Injectable } from '@angular/core';
import { HttpClient, HttpParams  } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

import { Veiculo } from '../veiculo/veiculo-model';

@Injectable({
  providedIn: 'root'
})


export class VeiculoService {
  
  private apiUrl = 'http://localhost:5000/api/'; // Substitua pela URL da sua API

  constructor(private http: HttpClient) { }

  // listarVeiculos(): Observable<any[]> {
  //   // return this.http.get<any[]>(`${this.apiUrl}/Locadoras`);
  //   return this.http.get<any[]>(this.apiUrl + 'Locadoras');

  // }

  listarVeiculos(): Observable<Veiculo[]> {
    return this.http.get<Veiculo[]>('http://localhost:5000/api/Veiculo'); // Ajuste o nome do seu controller aqui
  }

  filtrarVeiculos(
    locadoraNome: string,
    modeloId: number | null,
    dataCriacaoInicio: string,
    dataCriacaoFim: string
  ): Observable<Veiculo[]> {
    // Crie parâmetros de consulta
    let params = new HttpParams()
      .set('locadoraNome', locadoraNome)
      .set('modeloId', modeloId !== null ? modeloId.toString() : '')
      .set('dataCriacaoInicio', dataCriacaoInicio)
      .set('dataCriacaoFim', dataCriacaoFim);

    // Faça uma solicitação HTTP GET com os parâmetros de consulta
    return this.http.get<Veiculo[]>(`${this.apiUrl}Veiculo/filter`, { params });
  }

  obterVeiculoPorId(id: number): Observable<Veiculo> {
    return this.http.get<Veiculo>(`http://localhost:5000/api/Veiculo/${id}`);
  }

  // adicionarVeiculo(veiculo: Veiculo): Observable<Veiculo> {
  //   return this.http.post<Veiculo>('http://localhost:5000/api/Veiculo', veiculo);
  // }

  adicionarVeiculo(veiculo: Veiculo): Observable<Veiculo> {
    return this.http.post<Veiculo>('http://localhost:5000/api/Veiculo', veiculo)
      .pipe(
        catchError((error: any) => {
          if (error instanceof Error) {
            // Erro de cliente (por exemplo, conexão perdida)
            return throwError('Ocorreu um erro de cliente.');
          } else {
            // Erro no servidor (por exemplo, InvalidOperationException)
            return throwError(error.error); // Retorna a mensagem de erro do servidor
          }
        })
      );
  }

  atualizarVeiculo(veiculo: Veiculo, id: number): Observable<Veiculo> {
    veiculo.id= id;
    return this.http.put<Veiculo>(`http://localhost:5000/api/Veiculo`, veiculo);
  }


  excluirVeiculo(id: number): Observable<void> {
    const url = `http://localhost:5000/api/Veiculo?id=${id}`;
    return this.http.delete<void>(url);
  }
  
}
