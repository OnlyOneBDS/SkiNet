import { Component, Input, OnInit } from '@angular/core';
import { faShoppingCart } from '@fortawesome/free-solid-svg-icons';

import { IProduct } from 'src/app/shared/models/product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {
  @Input() product: IProduct;
  faShoppingCart = faShoppingCart;

  constructor() { }

  ngOnInit(): void {
  }
}
