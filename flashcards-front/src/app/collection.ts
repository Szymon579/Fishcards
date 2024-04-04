import { Card } from "./card";

export interface Collection {
    id: number;
    title: string;
    cards: Card[];
}