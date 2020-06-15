import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { environment } from '../../environments/environment';
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

    movie: Movie;
    rates: Array<Rate>;
    stars: Array<any>;

    userRate: Rate;
    @ViewChild('hearts', { static: false }) hearts: ElementRef;
    public readonly testImgUrl: String = `${environment.apiUrl}\\wwwroot\\elements\\IMG_20200524_114301.jpg`;

    constructor(private rateService: RateService, private activatedRoute: ActivatedRoute) {
        this.userRate = new Rate();
        this.userRate.score = 0;
    }

    ngOnInit() {
        const movieId = parseInt(this.activatedRoute.snapshot.paramMap.get('id'));
        this.fetchModel(movieId);
        console.log(this.hearts);
    }
    setStarsId(): void {
        this.stars = new Array<any>();
        this.stars.push(document.getElementById('h1'))
        this.stars.push(document.getElementById('h2'))
        this.stars.push(document.getElementById('h3'))
        this.stars.push(document.getElementById('h4'))
        this.stars.push(document.getElementById('h5'))
    }
    fetchModel(id: number): void {
        this.rateService.getMovieById(id).subscribe(
            result => {
                this.rates = result.rates;
                this.setMovieModel(result.element);
                this.setStarsId();
                this.userRate.elementId = this.movie.id;
            },
            error => console.error(error)
        )
    }

    setMovieModel(model: Movie) {
        this.movie = model;
        if (this.rates.length > 0)
            this.movie.score = this.rates.reduce(function (previousValue, currentValue, index, array) {
                return previousValue + currentValue.score;
            }, 0) / this.rates.length;
        else
            this.movie.score = 0;
        this.movie.imagePath = environment.apiUrl + this.movie.imagePath;
    }
    starClick(star: number) {
        this.userRate.score = star;
        this.hearts.nativeElement.childNodes.forEach(element => {
            element.style = "color: white;"
        });
        for (let index = 0; index < star; index++) {
            this.hearts.nativeElement.childNodes[index].style = "color: red;"
        }
    }

    sendRate() {
        this.rateService.sendRate(this.userRate).subscribe(
            result => {
                this.rates.push(result)
            },
            error =>
                console.error(error)
        );
    }
}
