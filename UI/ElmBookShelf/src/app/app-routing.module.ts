import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookListComponent } from './features/books/book-list/book-list.component';

const routes: Routes = [
  {
    path: 'book/index',
    component: BookListComponent
  },
  {
    path: '',
    component: BookListComponent
  },
  {
    path: 'book',
    component: BookListComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
