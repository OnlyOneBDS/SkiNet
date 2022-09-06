import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { SharedModule } from '../shared/shared.module';
import { ProductItemComponent } from './product-item/product-item.component';
import { ShopComponent } from './shop.component';

@NgModule({
  declarations: [
    ShopComponent,
    ProductItemComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    SharedModule
  ],
  exports: [ShopComponent]
})
export class ShopModule { }
