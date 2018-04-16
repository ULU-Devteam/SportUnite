import {Directive, ElementRef, OnInit, Renderer2, ViewChild} from '@angular/core';

@Directive({
  selector: '[appNotification]'
})
export class NotificationDirective implements OnInit {
  mobile: boolean;

  constructor(private el: ElementRef, private renderer: Renderer2) {
    this.onResize();
  }

  ngOnInit() {
    this.renderer.listen('window', 'resize', () => {this.onResize(); } );
    this.onResize();
  }

  onResize() {
    if (window.innerWidth > 768) {
      this.mobile = false;
      this.el.nativeElement.innerHTML = '<div data-toggle="dropdown"><span class="glyphicon glyphicon-bell"></span></div>';
    } else {
      this.mobile = true;
      this.el.nativeElement.innerHTML = '<div>Notificaties</div>';
    }
  }
}

