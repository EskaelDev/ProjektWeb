import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Injectable} from '@angular/core';
import {Movie} from '../_models/movie';

@Injectable({ providedIn: 'root' })
export class MovieService {
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<Movie[]>(`${environment.apiUrl}/movies`);
  }

  getSeveral(count: number) {
    return this.http.get<Movie[]>(`${environment.apiUrl}/movies`);
  }

  getAllContains(tags: string[]) {
    return this.http.get<Movie[]>(`${environment.apiUrl}/movies/tags`);
  }

  create(movie: Movie) {
    return this.http.post<Movie>(`${environment.apiUrl}/movies`, movie);
  }
}
