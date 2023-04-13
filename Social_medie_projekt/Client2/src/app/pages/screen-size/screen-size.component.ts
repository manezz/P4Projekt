import {
  Component,
  OnInit,
  HostListener,
  Output,
  EventEmitter,
  Input,
} from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-screen-size',
  standalone: true,
  template: ``,
  imports: [CommonModule],
})
export class ScreenSizeComponent implements OnInit {
  @Output() screenWidthChanged = new EventEmitter<number>();

  ngOnInit(): void {
    this.screenWidthChanged.emit(window.innerWidth);
  }

  @HostListener('window:resize', ['$event'])
  onResize(): void {
    this.screenWidthChanged.emit(window.innerWidth);
  }
}
