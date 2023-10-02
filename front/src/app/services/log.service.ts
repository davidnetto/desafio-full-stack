import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Log } from '../log/log-model';
@Injectable({
  providedIn: 'root'
})
export class LogService {

  constructor(private http: HttpClient) { }

  listar(): Observable<Log[]> {
    return this.http.get<Log[]>('http://localhost:5000/api/Log');
  }
}
