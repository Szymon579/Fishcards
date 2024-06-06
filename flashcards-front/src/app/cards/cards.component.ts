import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Card } from '../card';
import { CardService } from '../card.service';
import { Collection } from '../collection';

@Component({
  selector: 'app-cards',
  templateUrl: './cards.component.html',
  styleUrl: './cards.component.css'
})
export class CardsComponent implements OnInit {
  showFront: boolean = true;
  frontText: string | undefined = "Front of the flashcard";
  backText: string | undefined = "Back of the flashcard";
  cards: Card[] = [];
  index: number = 0;
  
  addingCard: boolean = false;
  addFront: string = "";
  addBack: string = "";

  editingCard: boolean = false;


  constructor(
    private cardService: CardService, 
    private location: Location, 
    private route: ActivatedRoute
    ) {}
  
  ngOnInit(): void {
    this.getFlashcards();   
  }

  getFlashcards(): void {
    this.cardService.getCardsByCollectionId(Number(this.route.snapshot.paramMap.get("id")))
    .subscribe(
      (cards: Card[]) => {
        this.cards = cards;
        this.setText();
        console.log(this.cards);
      });
  }

  addCard(frontText: string, backText: string): void {
    
    if(!frontText || !backText) {return;}
    
    const collId = Number(this.route.snapshot.paramMap.get("id"));
    console.log("collection id: " + collId);
    
    const newCard: Card = 
    {
      id: 0,
      collectionId: collId,
      frontText: frontText,
      backText: backText
    }

    this.cardService.addCard(newCard)
      .subscribe(() => this.cards.push(newCard));

    this.addingCard = false;
  }

  editCard(): void {
    if(this.editingCard) {
        const newCard: Card = {
          id: this.cards.at(this.index)!.id,
          collectionId: this.cards.at(this.index)!.collectionId,
          frontText: this.frontText!,
          backText: this.backText!
        };

        this.cards[this.index]= newCard;                    
        this.cardService.editCard(this.cards.at(this.index)!.id, newCard)
          .subscribe(() => {console.log("card edited");         
          });      
      }
    
    this.editingCard = !this.editingCard
    console.log("editing"); 
  }

  deleteCard(): void {
    const cardId = Number(this.cards[this.index].id);

    this.cards = this.cards.filter(card => card.id !== cardId);
    this.cardService.deleteCard(cardId).subscribe();
    this.showPrevious();
  }

  setText(): void {
    this.frontText = this.cards.at(this.index)?.frontText;
    this.backText = this.cards.at(this.index)?.backText;
  }

  showNext(): void {
    this.showFront = true;
    this.index += 1;
    if (this.index > this.cards.length - 1)
      this.index = 0;
    
    this.setText();
  }

  showPrevious(): void {
    this.showFront = true;
    this.index -= 1;
    if (this.index < 0)
      this.index = this.cards.length - 1;

    this.setText();
  }

  flipCard(): void {
    this.showFront = !this.showFront;
  }

  displayText(): string | undefined {
    return this.showFront ? this.frontText : this.backText; 
  }

  goBack(): void {
    this.location.back();
  }
}
