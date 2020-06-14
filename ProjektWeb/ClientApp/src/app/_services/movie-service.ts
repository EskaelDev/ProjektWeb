import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Injectable} from '@angular/core';
import {Movie} from '../_models/movie';
import {PathResult} from '../_models/path-result';
import {MovieReq} from '../_models/movie-req';
import {MoviesCount} from '../_models/movies-count';

@Injectable({ providedIn: 'root' })
export class MovieService {
  constructor(private http: HttpClient) { }
  private readonly controllerUrl: String = `${environment.apiUrl}/element/`;

  getAll(page: number, pageSize: number) {
    return this.http.get<Movie[]>(`${this.controllerUrl}all/${page}/${pageSize}`);
  }

  getCount() {
    return this.http.get<MoviesCount>(`${this.controllerUrl}count`);
  }

  getSeveral(count: number) {
    return this.http.get<Movie[]>(`${this.controllerUrl}`);
  }

  getAllContains(tags: string[]) {
    return this.http.get<Movie[]>(`${this.controllerUrl}`);
  }

  create(movie: MovieReq) {
    return this.http.post<Movie>(`${this.controllerUrl}save`, movie);
  }

  update(movie: MovieReq, movieId: number) {
    return this.http.put<Movie>(`${this.controllerUrl}${movieId}`, movie);
  }

  remove(movieId: string) {
    return this.http.delete<Movie>(`${this.controllerUrl}${movieId}`);
  }

  saveFile(data: FormData) {
    return this.http.post<PathResult>(`${this.controllerUrl}uploadfile`, data);
  }

  findMovieById(movieId: number) {
    return this.http.get<Movie>(`${this.controllerUrl}${movieId}`);
  }

  delete(movieId: number) {
    return this.http.delete<Movie>(`${this.controllerUrl}${movieId}`);
  }
}
