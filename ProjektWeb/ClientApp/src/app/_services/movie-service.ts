import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Injectable} from '@angular/core';
import {Movie} from '../_models/movie';

@Injectable({ providedIn: 'root' })
export class MovieService {
  constructor(private http: HttpClient) { }
  private readonly controllerUrl: String = `${environment.apiUrl}/element/`;

  getAll(page: number) {
    return this.http.get<Movie[]>(`${this.controllerUrl}all/${page}`);
  }

  getSeveral(count: number) {
    return this.http.get<Movie[]>(`${this.controllerUrl}`);
  }

  getAllContains(tags: string[]) {
    return this.http.get<Movie[]>(`${this.controllerUrl}`);
  }

  create(movie: Movie) {
    return this.http.post<Movie>(`${this.controllerUrl}`, movie);
  }

  remove(movieId: string) {
    return this.http.delete<Movie>(`${this.controllerUrl}${movieId}`);
  }

  saveFile(data: FormData) {
    return this.http.post<string>(`${this.controllerUrl}/UploadFile`, data);
  }
}
