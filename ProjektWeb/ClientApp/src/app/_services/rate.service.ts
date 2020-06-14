import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Rate } from '../_models/rate';
import { Movie } from '../_models/movie';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class RateService {

    private readonly controllerUrl: String = `${environment.apiUrl}/rate/`;
    constructor(private http: HttpClient) { }


    getRatesByElementId(id: number): Observable<Rate[]> {
        return this.http.get<Rate[]>(`${this.controllerUrl}all/${id}`);
    }

    getMovieById(id: number): Observable<Movie> {
        return this.http.get<Movie>(`${this.controllerUrl}/${id}`);
    }

}
