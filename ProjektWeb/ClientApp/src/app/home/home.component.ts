import { Component, OnInit } from '@angular/core';
import { Movie } from '../_models/movie';
import { MovieService } from '../_services/movie-service';
import { DialogComponent } from '../dialog/dialog.component';
import { MatDialog } from '@angular/material';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  movies: Movie[];

  constructor(private movieService: MovieService, private dialog: MatDialog) {
    this.movies = new Array<Movie>();
    movieService.getAll(0).subscribe(
      data => {
        if (data && data.length > 0) {
          data.forEach(element => {
            this.movies.push(element);
          });
        }
      },
      error => this.openDialog(error)
    );
  }

  ngOnInit() {
  }

  private openDialog(error: string) {
    this.dialog.open(DialogComponent, {
      width: '30%',
      minHeight: '200px',
      data: { title: 'Error', content: error }
    });
  }
}
