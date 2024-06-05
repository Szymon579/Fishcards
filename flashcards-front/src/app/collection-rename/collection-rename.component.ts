import { Component, Output } from '@angular/core';
import { EventEmitter } from '@angular/core'

@Component({
  selector: 'app-collection-rename',
  templateUrl: './collection-rename.component.html',
  styleUrl: './collection-rename.component.css'
})
export class CollectionRenameComponent {
  @Output() cancelEvent = new EventEmitter();
  @Output() submitEvent = new EventEmitter<string>();

  onSubmit(newName: string): void {
    console.log("renamed");
    this.submitEvent.emit(newName);
  }

  onCancel(): void {
    this.cancelEvent.emit();
  }
}
