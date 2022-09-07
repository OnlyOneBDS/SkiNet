import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { faMinusCircle, faPlusCircle } from "@fortawesome/free-solid-svg-icons";
import { BreadcrumbService } from "xng-breadcrumb";

import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  faMinusCircle = faMinusCircle;
  faPlusCircle = faPlusCircle;
  product: IProduct;

  constructor(private shopService: ShopService, private activatedRouted: ActivatedRoute, private bcService: BreadcrumbService) {
    this.bcService.set("@productDetails", " ");
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    this.shopService.getProduct(+this.activatedRouted.snapshot.paramMap.get("id"))
      .subscribe({
        next: (product) => {
          this.product = product;
          this.bcService.set("@productDetails", product.name);
        },
        error: (e) => console.log(e.error)
      });
  }
}
