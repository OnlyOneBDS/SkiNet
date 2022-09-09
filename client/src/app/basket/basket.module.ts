import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { SharedModule } from '../shared/shared.module';
import { BasketRoutingModule } from './basket-routing.module';
import { BasketComponent } from './basket.component';

@NgModule({
  declarations: [
    BasketComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    BasketRoutingModule,
    SharedModule
  ]
})
export class BasketModule { }