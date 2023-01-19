import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataService } from '../_services/data.service';


@Component({
  selector: 'app-indexpage',
  template: `    
  <!-- looper igennem alle post fra data(DataService) -->
  <div id="post" *ngFor="let data of data">
    <h1 id="title">{{data.title}}</h1>
    <h5 id="username">{{data.username}}</h5>
    <h3 id="description">{{data.desc}}</h3>
    <p id="date">{{data.date}}</p>
    <p id="tags">{{data.tag}} </p>
    <button class="postBtn" id="like"><3</button>
  </div>


  
  `,
  styleUrls: ["../_css/poststyle.css"]
  
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
