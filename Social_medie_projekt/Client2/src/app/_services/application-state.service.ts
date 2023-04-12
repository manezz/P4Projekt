import { HostListener, Injectable, OnInit } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ApplicationStateService implements OnInit {
  private screenWidth: number = 0;

  constructor() {}

  ngOnInit(): void {
    this.screenWidth = window.innerWidth;
  }

  public getScreenWidth(): number {
    return this.screenWidth;
  }

  @HostListener('window:resize', ['$event'])
  onWindowResize() {
    this.screenWidth = window.innerWidth;
    console.log(window.innerWidth);
  }
}
