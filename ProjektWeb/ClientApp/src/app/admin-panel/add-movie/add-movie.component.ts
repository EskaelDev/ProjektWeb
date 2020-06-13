import {Component, Input, OnInit} from '@angular/core';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import {Movie} from '../../_models/movie';
import {MatChipInputEvent} from '@angular/material';
import {FormBuilder, Validators} from '@angular/forms';

@Component({
  selector: 'app-add-movie',
  templateUrl: './add-movie.component.html',
  styleUrls: ['./add-movie.component.css']
})
export class AddMovieComponent {

  @Input()
  movie: Movie;
  loading = false;

  selectable = true;
  removable = true;
  addOnBlur = true;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  tags: string[] = ['comedy'];

  createForm = this.fb.group({
    title: ['', Validators.required],
    description: ['', Validators.required],
    file: ['']
  });

  constructor(private fb: FormBuilder) {
    // this.filteredFruits = this.tagCtrl.valueChanges.pipe(
    //   startWith(null),
    //   map((fruit: string | null) => fruit ? this._filter(fruit) : this.allFruits.slice()));
  }

  save() {

  }

  isFormValid() {
    return false;
  }

  add(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    // Add our fruit
    if ((value || '').trim()) {
      this.tags.push(value.trim());
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }
  }

  remove(tag: string): void {
    const index = this.tags.indexOf(tag);

    if (index >= 0) {
      this.tags.splice(index, 1);
    }
  }
}
