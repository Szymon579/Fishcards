import { Injectable } from '@angular/core';
import { Collection, ShareCollection } from './collection';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CollectionService {
  
  private collectionsUrl = "http://localhost:5243/api/collections"
  httpOptions = {
    headers: new HttpHeaders({"Content-type": "application/json"})
  };

  constructor(private http: HttpClient) { }

  getCollections(): Observable<Collection[]> {
    return this.http.get<Collection[]>(this.collectionsUrl)
  }

  getCollection(id: number): Observable<Collection> {
    return this.http.get<Collection>(this.collectionsUrl + `/${id}`);
  }

  addCollection(newCollection: Collection): Observable<Collection> { 
    return this.http.post<Collection>(this.collectionsUrl, newCollection, this.httpOptions);
  }

  deleteCollection(id: number): Observable<Collection> {
    return this.http.delete<Collection>(this.collectionsUrl + `/${id}`);
  }

  shareCollection(share: ShareCollection): Observable<Collection> {
    console.log(share);
    return this.http.post<Collection>(this.collectionsUrl + `/share`, share, this.httpOptions);
  }

  getSharedCollections(): Observable<Collection[]> {
    return this.http.get<Collection[]>(this.collectionsUrl + `/shared`);
  }

}
