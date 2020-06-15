import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Rate } from '../_models/rate';
import { Movie } from '../_models/movie';
import { Observable } from 'rxjs';
import { MovieRate } from '../_models/movie-rate';

@Injectable({
    providedIn: 'root'
})
export class RateService {

    private readonly elementControllerUrl: String = `${environment.apiUrl}/Element`;
    private readonly rateControllerUrl: String = `${environment.apiUrl}/Rate`;
    constructor(private http: HttpClient) { }


    getRatesByElementId(id: number): Observable<Rate[]> {
        return this.http.get<Rate[]>(`${this.elementControllerUrl}all/${id}`);
    }

    getMovieById(id: number): Observable<MovieRate> {
        return this.http.get<MovieRate>(`${this.elementControllerUrl}/${id}/details`);
    }

    sendRate(rate:Rate): Observable<Rate> {
        return this.http.post<Rate>(`${this.rateControllerUrl}/add`, rate);
    }
}
