import { Component, OnDestroy, SecurityContext } from '@angular/core';
import { SearchBooksRequest } from '../../models/book-search-request.model';
import { BookService } from '../../services/book.service';
import { Subscription } from 'rxjs';
import { SearchBooksResponse } from '../../models/book-search-response.model';
import { DomSanitizer } from '@angular/platform-browser';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnDestroy{
  model: SearchBooksRequest;
  data: SearchBooksResponse[] = [];  
  loading = false;

  private searchBooksSubscription?: Subscription

  constructor(private bookService: BookService, private sanitizer: DomSanitizer, public datePipe: DatePipe){
    this.model = {
      searchKey: "",
      page: 1,
      pageSize: 10
    };
  }
 
  ngOnInit() {
    this.fetchData();
    window.addEventListener('scroll', this.onScroll, true);
  }

  fetchData(){  
    this.loading = true;

    this.searchBooksSubscription = this.bookService.searchBooks(this.model).subscribe({
      next: (response) => {
        this.data.push(...response);
        this.loading = false;
        console.log(response);
      },
      error: (error) => {
        console.error('Error fetching data:', error);
        this.loading = false;
      }
    }); 
  }

  filterData() { 
    this.model.searchKey = this.model.searchKey.trim();

    if(this.model.searchKey == '') { 
      this.model.page = 1; 
    }

    this.model.searchKey = this.sanitizer.sanitize(SecurityContext.HTML, this.model.searchKey)?? ""; 

    this.data = [];
    this.fetchData();
    return;
  }

  onScroll = (): void => {
    if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight) {
      this.model.page++;
      this.fetchData();
    }
  }

  ngOnDestroy(): void { 
    this.searchBooksSubscription?.unsubscribe();
    window.removeEventListener('scroll', this.onScroll, true);
  }
}
