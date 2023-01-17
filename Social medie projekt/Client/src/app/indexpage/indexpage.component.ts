import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataService } from '../_services/data.service';
import { Data } from '@angular/router';


@Component({
  selector: 'app-indexpage',
  // standalone: false,
  // imports: [CommonModule],

  template: `    
  <!-- looper igennem alle post fra data(DataService) -->
  <div id="post" *ngFor="let data of data">
    <h1 id="title">{{data.title}}</h1>
    <h5 id="username">{{data.username}}</h5>
    <h3 id="description">{{data.desc}}</h3>
    <p id="date">{{data.date}}</p>
    <p id="tags">{{data.tag}} </p>
  </div>

  `,
  styles: [`
  h1{
    color: white;
  }
  
  #post{
    position: relative;
    width: auto;
    max-width: 35%;
    border: 1px solid white;
    border-radius: 25px;
    margin: auto;
    margin-top: 10px; 
    margin-bottom: 60px;
    padding: 20px;
  }
  `]
  
})
export class IndexpageComponent implements OnInit {

  data
  // sætter values i getTempData til data
  constructor(data:DataService){
    this.data = data.getTempData()
  }

  ngOnInit(): void {

  }

}
