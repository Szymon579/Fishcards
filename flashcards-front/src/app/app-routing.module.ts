import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CollectionsComponent } from './collections/collections.component';
import { CardsComponent } from './cards/cards.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  {path: '', redirectTo: "/login", pathMatch: "full"},
  {path: 'collections', component: CollectionsComponent},
  {path: 'collections/:id', component: CardsComponent},
  {path: 'login', component: LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
