import { Directive, ElementRef, HostListener, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appHighlight]',
  standalone: true // optional modern Angular
})
export class HighlightDirective {

  constructor(
    private el: ElementRef,
    private renderer: Renderer2
  ) {}

  @HostListener('mouseenter')
  onMouseEnter(): void {
    this.renderer.setStyle(
      this.el.nativeElement,
      'background-color',
      '#f5f5f5'
    );

    this.renderer.setStyle(
      this.el.nativeElement,
      'transition',
      '0.2s ease'
    );
  }

  @HostListener('mouseleave')
  onMouseLeave(): void {
    this.renderer.removeStyle(this.el.nativeElement, 'background-color');
  }
}