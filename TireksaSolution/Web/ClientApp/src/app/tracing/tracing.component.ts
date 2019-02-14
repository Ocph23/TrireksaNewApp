import { Component, AfterViewInit } from '@angular/core';
import * as anime from 'animejs';

import { of, merge } from 'rxjs';
import { mapTo, delay } from 'rxjs/operators';



@Component({
  selector: 'app-tracing',
  templateUrl: './tracing.component.html',
  styleUrls: ['./tracing.component.scss']
})
export class TracingComponent implements AfterViewInit {
haveResult:boolean=false;
dataCount=1;
  constructor() { }
  ngAfterViewInit(): void {
    anime({
      targets: 'section #tracing-box',
      translateY: 200,
      duration:500,
      delay:1000
    });
  }

 public FoundResult(){
    if(this.dataCount>0)
    {
      this.dataCount=0;
      anime({
        targets: 'section #tracing-box',
        translateY: 200,
        duration:500,
        delay:100
      });
      of(this.ShowData(this.dataCount),delay(1000));
   
    }else{
      this.dataCount=2;
     
      anime({
        targets: 'section #tracing-box',
        translateY:0,
        duration:500,
        delay:100
      });
      of(this.ShowData(this.dataCount),delay(1000));
   
    }
    
  }
  

  ShowData(data){
    var result=false;
    if(data>0)
    result=true;
  this.haveResult=result;
  }
  
}
