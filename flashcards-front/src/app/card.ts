export interface Card {
    id: number;
    collectionId: number;
    frontText: string;
    backText: string;
}

export interface NewCard {
    collectionId: number;
    frontText: string,
    backText: string
}
