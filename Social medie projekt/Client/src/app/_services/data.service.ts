import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor() { }

  getTempData(){
    return[
      {
        id: 1,
        posterId: 1,
        username: "user 1",
        title: "Title",
        desc: "description",
        date: "01-01-23",
        tag: ['test', 'tester', 'testing']
      },
      {
        id: 4,
        posterId: 1,
        username: "user 1",
        title: "Titleeeeee",
        desc: "descriptionnnnnn",
        date: "01-01-23",
        tag: ['test', 'testerrrrrrrrrrrrrrrr', 'testing']
      },
      {
        id: 2,
        posterId: 2,
        username: "user 2",
        title: "Title 2",
        desc: "description 2",
        date: "02-02-23",
        tag: ['test 2', 'tester 2', 'testing 2']
      },
      {
        id: 3,
        posterId: 3,
        username: "user 3",
        title: "Title 3",
        date: "03-03-23",
      }
    ]
  }

}
