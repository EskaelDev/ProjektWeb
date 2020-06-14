import {Component, Input, OnInit} from '@angular/core';
import {Movie} from '../../_models/movie';
import {environment} from '../../../environments/environment';

@Component({
  selector: 'app-movie-panel',
  templateUrl: './movie-panel.component.html',
  styleUrls: ['./movie-panel.component.css']
})
export class MoviePanelComponent implements OnInit {

  @Input()
  movie: Movie;
  env = environment;

  constructor() { }

  ngOnInit() {
  }

}
