import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

import { Montadora } from '../montadora/montadora-model';

@Injectable({
  providedIn: 'root'
})


export class MontadoraService {
  
  private apiUrl = 'http://localhost:5000/api/'; // Substitua pela URL da sua API

  constructor(private http: HttpClient) { }

  // listarMontadoras(): Observable<any[]> {
  //   // return this.http.get<any[]>(`${this.apiUrl}/Locadoras`);
  //   return this.http.get<any[]>(this.apiUrl + 'Locadoras');

  // }

  listarMontadoras(): Observable<Montadora[]> {
    return this.http.get<Montadora[]>('http://localhost:5000/api/Montadora'); // Ajuste o nome do seu controller aqui
  }

  obterMontadoraPorId(id: number): Observable<Montadora> {
    return this.http.get<Montadora>(`http://localhost:5000/api/Montadora/${id}`);
  }

  // adicionarMontadora(montadora: Montadora): Observable<Montadora> {
  //   return this.http.post<Montadora>('http://localhost:5000/api/Montadora', montadora);
  // }

  adicionarMontadora(montadora: Montadora): Observable<Montadora> {
    return this.http.post<Montadora>('http://localhost:5000/api/Montadora', montadora)
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

  atualizarMontadora(montadora: Montadora, id: number): Observable<Montadora> {
    montadora.id= id;
    return this.http.put<Montadora>(`http://localhost:5000/api/Montadora`, montadora);
  }


  excluirMontadora(id: number): Observable<void> {
    const url = `http://localhost:5000/api/Montadora?id=${id}`;
    return this.http.delete<void>(url);
  }
  
}
