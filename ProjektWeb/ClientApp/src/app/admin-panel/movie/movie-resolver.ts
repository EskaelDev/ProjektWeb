import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {Movie} from '../../_models/movie';
import {MovieService} from '../../_services/movie-service';

@Injectable({
  providedIn: 'root'
})

export class MovieResolver implements Resolve<Movie | null> {

  constructor(private readonly movieService: MovieService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Movie | null> | Movie | null {
    return this.movieService.findMovieById(route.params.movieId);
  }
}
