import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor() { }

  getTempData(){
    return[
      {
        postId: 1,
        userId: 1,
        username: "user 1",
        title: "Title",
        desc: "description",
        date: "01-01-23",
        tag: ['test', 'tester', 'testing'],
        likedByUser: false
      },
      {
        postId: 4,
        userId: 1,
        username: "user 1",
        title: "Titleeeeee",
        desc: "descriptionnnnnn",
        date: "01-01-23",
        tag: ['test', 'testerrrrrrrrrrrrrrrr', 'testing'],
        likedByUser: false
      },
      {
        postId: 2,
        userId: 2,
        username: "user 2",
        title: "Title 2",
        desc: "description 2",
        date: "02-02-23",
        tag: ['test 2', 'tester 2', 'testing 2'],
        likedByUser: true
      },
      {
        postId: 3,
        userId: 3,
        username: "user 3",
        title: "Title 3",
        date: "03-03-23",
        likedByUser: true
      }
    ]
  }

}
