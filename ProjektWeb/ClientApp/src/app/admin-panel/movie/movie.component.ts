import {AfterViewInit, Component, OnInit} from '@angular/core';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import {Movie} from '../../_models/movie';
import {MatChipInputEvent} from '@angular/material';
import {FormBuilder, Validators} from '@angular/forms';
import {MovieService} from '../../_services/movie-service';
import {ActivatedRoute, Router} from '@angular/router';
import {Tag} from '../../_models/tag';
import {MovieReq} from '../../_models/movie-req';
import {map} from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-add-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent implements AfterViewInit {

  movie: Movie;
  loading = false;

  imagePath;
  imgURL: any = '../../../assets/img/placeholder-img.png';
  message: string;
  movieFile: any;

  selectable = true;
  removable = true;
  addOnBlur = true;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  tags: Tag[] = this.movie && this.movie.tags ? this.movie.tags : [];

  createForm = this.fb.group({
    title: [this.movie ? this.movie.title : '', Validators.required],
    description: [this.movie ? this.movie.description : '', Validators.required]
  });

  constructor(private fb: FormBuilder, private movieService: MovieService,
              private router: Router, private route: ActivatedRoute) {
  }

  save() {
    const nMovie: MovieReq = new MovieReq(); 

    nMovie.title = this.createForm.controls.title.value;
    nMovie.description = this.createForm.controls.description.value;
    nMovie.tags = this.tags.map(tag => tag.name);

    if (this.movieFile) {
      const data = new FormData();
      data.append('uploadFile', this.movieFile, nMovie.title);

      this.movieService.saveFile(data)
        .subscribe(
          result => {
            nMovie.imagePath = result.path;
            if (this.movie) {
              this.movieService.update(nMovie, this.movie.id).subscribe(
                movie => this.router.navigate(['/admin'])
              );
            } else {
              this.movieService.create(nMovie).subscribe(
                movie => this.router.navigate(['/admin'])
              );
            }
          });
    } else {
      nMovie.imagePath = this.movie.imagePath;
      this.movieService.update(nMovie, this.movie.id).subscribe(
        movie => this.router.navigate(['/admin'])
      );
    }
  }

  isFormValid() {
    return this.createForm.controls.title.valid && this.createForm.controls.description.valid && (this.movie || this.movieFile);
  }

  add(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    // Add our tag
    if ((value || '').trim()) {
      const nTag: Tag = {
        name: value.trim()
      };
      this.tags.push(nTag);
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }
  }

  remove(tag: Tag): void {
    const index = this.tags.indexOf(tag);

    if (index >= 0) {
      this.tags.splice(index, 1);
    }
  }

  preview(files) {
    if (files.length === 0) {
      return;
    }

    const mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.message = 'Only images are supported.';
      return;
    }

    this.movieFile = files[0];
    const reader = new FileReader();
    this.imagePath = files;
    reader.readAsDataURL(this.movieFile);
    reader.onload = (_event) => {
      this.imgURL = reader.result;
    };
  }

  ngAfterViewInit(): void {
    this.route.params.pipe(map(p => p.movieId)).subscribe(id => {
      this.movieService.findMovieById(id).subscribe(
        movie => {
          this.movie = movie;
          this.tags = movie.tags ? movie.tags : [];
          this.imgURL = environment.staticFilesUrl + movie.imagePath;
        }
      ); });
  }
}
