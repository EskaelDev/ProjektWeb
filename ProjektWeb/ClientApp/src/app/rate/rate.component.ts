import { Component, OnInit, Input } from '@angular/core';
import { Rate } from '../_models/rate';

@Component({
    selector: 'app-rate',
    templateUrl: './rate.component.html',
    styleUrls: ['./rate.component.css']
})
export class RateComponent implements OnInit {

    @Input() rate: Rate;
    constructor() { }

    ngOnInit() {
    }

}
