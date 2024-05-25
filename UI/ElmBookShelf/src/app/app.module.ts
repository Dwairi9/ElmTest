import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { BookListComponent } from './features/books/book-list/book-list.component';
import { FormsModule } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http'
import { DatePipe } from '@angular/common';
import { LoadingSpinnerComponent } from './core/components/loading-spinner/loading-spinner.component';
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    BookListComponent,
    LoadingSpinnerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
