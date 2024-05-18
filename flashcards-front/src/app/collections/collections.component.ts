import { Component, OnInit } from '@angular/core';
import { Collection, ShareCollection } from '../collection';
import { CollectionService } from '../collection.service';

@Component({
  selector: 'app-collections',
  templateUrl: './collections.component.html',
  styleUrl: './collections.component.css'
})
export class CollectionsComponent implements OnInit {
  collections: Collection[] = [];
  isPopupVisible: boolean = false;
  collectionId: number = 0;

  constructor(private collectionService: CollectionService) {}

  ngOnInit(): void {
    this.getCollections();
    this.getSharedCollections();
  }

  getCollections(): void {
    this.collectionService.getCollections()
      .subscribe(collections => {this.collections = collections;  console.log(collections);
      });
  }

  getSharedCollections(): void {
    this.collectionService.getSharedCollections()
      .subscribe(shared => {this.collections = this.collections.concat(shared); console.log(shared);
      });
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

  displayPopup(collectionId: number): void {
    this.isPopupVisible = true;
    this.collectionId = collectionId;
  }

  closePopup(): void {
    this.isPopupVisible = false;
  }

  shareCollection(collectionId: number, email: string) : void {
    const newShare: ShareCollection = {
      id: collectionId,
      email: email
    }

    this.collectionService.shareCollection(newShare).subscribe(() => this.isPopupVisible = false);
  }
}
