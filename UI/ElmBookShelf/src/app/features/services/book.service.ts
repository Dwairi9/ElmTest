import { Injectable } from '@angular/core';
import { SearchBooksRequest } from '../models/book-search-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { SearchBooksResponse } from '../models/book-search-response.model';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(private http: HttpClient) {

   }

  searchBooks(model: SearchBooksRequest): Observable<SearchBooksResponse[]> {
    return this.http.post<SearchBooksResponse[]>('https://localhost:7160/api/book/GetBooks', model);
  }
}
