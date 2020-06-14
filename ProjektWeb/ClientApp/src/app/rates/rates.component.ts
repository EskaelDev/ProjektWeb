import { Component, OnInit } from '@angular/core';
import { environment } from '../../environments/environment';
import {
    MatFormFieldModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
} from '@angular/material';
import { Rate } from '../_models/rate';
import { Movie } from '../_models/movie';
import { RateService } from '../_services/rate.service';
import { ActivatedRoute } from '@angular/router';


@Component({
    selector: 'app-rates',
    templateUrl: './rates.component.html',
    styleUrls: ['./rates.component.css']
})
export class RatesComponent implements OnInit {

    private readonly controllerUrl: String = `${environment.apiUrl}`;

    model: Movie;
    rates: Array<Rate>;

    public readonly testImgUrl: String = `${environment.apiUrl}\\wwwroot\\elements\\IMG_20200524_114301.jpg`;

    constructor(private rateService: RateService, private activatedRoute: ActivatedRoute) {

    }

    ngOnInit() {
        const movieId = parseInt(this.activatedRoute.snapshot.paramMap.get('id'));
        this.fetchModel(movieId);
    }

    fetchModel(id: number): void {
        this.rateService.getMovieById(id).subscribe(
            result => {
                this.model = result;
                this.fetchRates(result.id);
            },
            error => console.error(error)
        )

    }

    fetchRates(id: number): void {
        this.rateService.getRatesByElementId(this.model.id).subscribe(
            result => this.rates = result,
            error => console.error(error)
        )
    }
}
