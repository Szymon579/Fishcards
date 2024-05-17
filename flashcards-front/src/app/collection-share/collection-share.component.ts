import { Component, Output } from '@angular/core';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-collection-share',
  templateUrl: './collection-share.component.html',
  styleUrl: './collection-share.component.css'
})

export class CollectionShareComponent {
  @Output() cancelEvent = new EventEmitter();
  email: string = '';

  onSubmit(): void {
    if(this.email) {
      console.log('Email: ', this.email);
    }
  }

  onCancel(): void {
    this.cancelEvent.emit();
  }

}
