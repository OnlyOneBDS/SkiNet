import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { SharedModule } from '../shared/shared.module';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { ShopRoutingModule } from './shop-routing.module';
import { ShopComponent } from './shop.component';

@NgModule({
  declarations: [
    ProductDetailsComponent,
    ProductItemComponent,
    ShopComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    SharedModule,
    ShopRoutingModule
  ]
})
export class ShopModule { }
