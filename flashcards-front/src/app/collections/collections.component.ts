import { Component, OnInit } from '@angular/core';
import { Collection, ShareCollection, RenameCollection } from '../collection';
import { CollectionService } from '../collection.service';

@Component({
  selector: 'app-collections',
  templateUrl: './collections.component.html',
  styleUrl: './collections.component.css'
})
export class CollectionsComponent implements OnInit {
  
  collections: Collection[] = [];
  renamePopup: boolean = false;
  sharePopup: boolean = false;
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
    this.collectionService.deleteCollection(id).subscribe();
    
  }

  renameCollection(collectionId: number, newTitle: string): void {
    const updatedCollection: RenameCollection = {
      id: collectionId,
      title: newTitle
    }
    
    this.collectionService.renameCollection(updatedCollection)
      .subscribe(() => {
                        this.renamePopup = false; 
                        const coll = this.collections.find(c => c.id == collectionId);
                        if(coll)
                          coll.title = newTitle
                        else
                          console.log("Rename error");  
                        });
  }

  shareCollection(collectionId: number, email: string) : void {
    const newShare: ShareCollection = {
      id: collectionId,
      email: email
    }
    this.collectionService.shareCollection(newShare)
      .subscribe(() => this.sharePopup = false );
  }

  displaySharePopup(id: number): void {
    this.sharePopup = true;
    this.collectionId = id;
  }

  displayRenamePopup(collectionId: number): void {
    this.renamePopup = true;
    this.collectionId = collectionId;
  }

  closePopup(): void {
    this.renamePopup = false;
    this.sharePopup = false;
  }
}
