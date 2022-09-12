import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { CarouselModule } from "ngx-bootstrap/carousel";
import { BsDropdownModule } from "ngx-bootstrap/dropdown";
import { PaginationModule } from 'ngx-bootstrap/pagination';

import { OrderTotalsComponent } from './components/order-totals/order-totals.component';
import { PagerComponent } from './components/pager/pager.component';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { TextInputComponent } from './components/text-input/text-input.component';


@NgModule({
  declarations: [
    PagerComponent,
    PagingHeaderComponent,
    OrderTotalsComponent,
    TextInputComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    CarouselModule.forRoot(),
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot()
  ],
  exports: [
    CarouselModule,
    ReactiveFormsModule,
    BsDropdownModule,
    PaginationModule,
    OrderTotalsComponent,
    PagerComponent,
    PagingHeaderComponent,
    TextInputComponent
  ]
})
export class SharedModule { }
