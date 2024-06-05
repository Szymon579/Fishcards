import { Component, Output } from '@angular/core';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-collection-share',
  templateUrl: './collection-share.component.html',
  styleUrl: './collection-share.component.css'
})

export class CollectionShareComponent {
  @Output() cancelEvent = new EventEmitter();
  @Output() submitEvent = new EventEmitter<string>();

  onSubmit(email: string): void {
    console.log("submited");     
    this.submitEvent.emit(email);
  }

  onCancel(): void {
    this.cancelEvent.emit();
  }

}
