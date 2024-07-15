import { Injectable } from '@angular/core';
import { Card, NewCard } from './card';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, map, pipe } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CardService {
  private cardsUrl = "http://localhost:5243/api/cards";
  private cardsByCollectionUrl = "http://localhost:5243/api/cards";
  httpOptions = {
    headers: new HttpHeaders({"Content-type": "application/json"})
  };

  constructor(private http: HttpClient) { }

  getCardsByCollectionId(collectionId: number): Observable<Card[]> {
    return this.http.get<Card[]>(this.cardsByCollectionUrl + `/${collectionId}`);
  }

  addCard(card: NewCard): Observable<NewCard> {
    console.log("in service");
    console.log(card);
    return this.http.post<NewCard>(this.cardsUrl, card, this.httpOptions);
  }

  editCard(id: number, card: Card): Observable<Card> {
    return this.http.put<Card>(this.cardsUrl + `/${id}`, card, this.httpOptions);
  }

  deleteCard(id: number): Observable<Card> {
    return this.http.delete<Card>(this.cardsUrl + `/${id}`);
  }
  
}
