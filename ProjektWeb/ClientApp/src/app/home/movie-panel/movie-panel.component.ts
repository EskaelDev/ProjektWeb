import {Component, Input, OnInit} from '@angular/core';
import {Movie} from '../../_models/movie';

@Component({
  selector: 'app-movie-panel',
  templateUrl: './movie-panel.component.html',
  styleUrls: ['./movie-panel.component.css']
})
export class MoviePanelComponent implements OnInit {

  @Input()
  movie: Movie;

  constructor() { }

  ngOnInit() {
  }

}
