import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CollectionsComponent } from './collections/collections.component';
import { CardsComponent } from './cards/cards.component';

const routes: Routes = [
  {path: '', redirectTo: "/collections", pathMatch: "full"},
  {path: 'collections', component: CollectionsComponent},
  {path: 'collections/:id', component: CardsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
