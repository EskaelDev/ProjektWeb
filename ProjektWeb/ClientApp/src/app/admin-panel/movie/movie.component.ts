import {Component, Input, OnInit} from '@angular/core';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import {Movie} from '../../_models/movie';
import {MatChipInputEvent} from '@angular/material';
import {FormBuilder, Validators} from '@angular/forms';
import {MovieService} from '../../_services/movie-service';
import {Router} from '@angular/router';
import {Tag} from '../../_models/tag';

@Component({
  selector: 'app-add-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent implements OnInit{

  @Input()
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
  tags: Tag[] = this.movie ? this.movie.tags : [];

  createForm = this.fb.group({
    title: ['', Validators.required],
    description: ['', Validators.required]
  });

  constructor(private fb: FormBuilder, private movieService: MovieService,
              private router: Router) {}

  save() {
    if (!this.movie) {
      this.movie = new Movie();
    }
    this.movie.title = this.createForm.controls.title.value;
    this.movie.description = this.createForm.controls.description.value;
    this.movie.tags = this.tags;

    const data = new FormData();
    data.append('uploadFile', this.movieFile, this.movie.title);
    this.movieService.saveFile(data)
      .subscribe(
        result => {
          this.movie.imagePath = result.path;
            this.movieService.create(this.movie).subscribe(
              movie => this.router.navigate(['/admin'])
            );
        });
  }

  isFormValid() {
    return this.createForm.controls.title.valid && this.createForm.controls.description.valid &&
      this.movieFile;
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

  ngOnInit(): void {
    if (this.movie)
    {

    }
  }
}
