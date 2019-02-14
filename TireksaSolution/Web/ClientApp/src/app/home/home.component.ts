import { Component, AfterViewInit } from '@angular/core';
import * as anime from 'animejs';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements AfterViewInit {
  titleText: string;
  square: any;

  constructor() {
    this.titleText="Test Saja";
   }

   ngAfterViewInit(){
    anime({
      targets: 'section .card',
      rotate: '1turn',
      duration:500,
      delay:1000
    });
   }
  

}
