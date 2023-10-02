import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

import { Modelo } from '../modelo/modelo-model';

@Injectable({
  providedIn: 'root'
})


export class ModeloService {
  
  private apiUrl = 'http://localhost:5000/api/'; // Substitua pela URL da sua API

  constructor(private http: HttpClient) { }

  listarModelos(): Observable<Modelo[]> {
    return this.http.get<Modelo[]>('http://localhost:5000/api/Modelo'); // Ajuste o nome do seu controller aqui
  }

  obterModeloPorId(id: number): Observable<Modelo> {
    return this.http.get<Modelo>(`http://localhost:5000/api/Modelo/${id}`);
  }

  adicionarModelo(modelo: Modelo): Observable<Modelo> {
    return this.http.post<Modelo>('http://localhost:5000/api/Modelo', modelo)
      .pipe(
        catchError((error: any) => {
          if (error instanceof Error) {
            return throwError('Ocorreu um erro de cliente.');
          } else {
            return throwError(error.error); 
          }
        })
      );
  }

  atualizarModelo(modelo: Modelo, id: number): Observable<Modelo> {
    modelo.id= id;
    modelo = modelo;
    return this.http.put<Modelo>(`http://localhost:5000/api/Modelo`, modelo);
  }


  excluirModelo(id: number): Observable<void> {
    const url = `http://localhost:5000/api/Modelo?id=${id}`;
    return this.http.delete<void>(url);
  }
  
}
