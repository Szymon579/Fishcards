import { Component, OnInit } from '@angular/core';
import { Collection } from '../collection';
import { CollectionService } from '../collection.service';

@Component({
  selector: 'app-collections',
  templateUrl: './collections.component.html',
  styleUrl: './collections.component.css'
})
export class CollectionsComponent implements OnInit {
  collections: Collection[] = [];
  isPopupVisible: boolean = false;

  constructor(private collectionService: CollectionService) {}

  ngOnInit(): void {
    this.getCollections();
  }

  getCollections(): void {
    this.collectionService.getCollections()
      .subscribe(collections => {this.collections = collections;  console.log(collections)});
  }

  addCollection(name: string): void {
    if(!name) {return;}
    const newCollection: Collection = 
    {
      id: 0,
      title: name,
      cards: []
    }
    this.collectionService.addCollection(newCollection)
      .subscribe(coll => this.collections.push(newCollection));
  }

  deleteCollection(id: number): void {
    this.collections = this.collections.filter(deletedColl => deletedColl.id !== id);
    this.collectionService.deleteCollection(id)
      .subscribe();
    
  }

  sharePopup(display: boolean): void {
    this.isPopupVisible = display;
  }
}
