import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';

import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild("search", { static: false }) searchTearm: ElementRef;
  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  shopParams: ShopParams;
  totalCount: number;
  sortOptions = [
    { name: "Alphabetical", value: "name" },
    { name: "Price: Low to High", value: "priceAsc" },
    { name: "Price: High to Low", value: "priceDesc" }
  ];

  constructor(private shopService: ShopService) {
    this.shopParams = shopService.getShopParams();
  }

  ngOnInit(): void {
    this.getProducts(true);
    this.getBrands();
    this.getTypes();
  }

  getProducts(useCache = false) {
    this.shopService
      .getProducts(useCache)
      .subscribe({
        next: (resp) => {
          this.products = resp.data;
          this.totalCount = resp.count;
        },
        error: (e) => console.log(e)
      })
  }

  getBrands() {
    this.shopService
      .getBrands()
      .subscribe({
        next: (resp) => this.brands = [{ id: 0, name: "All" }, ...resp],
        error: (e) => console.log(e)
      })
  }

  getTypes() {
    this.shopService
      .getTypes()
      .subscribe({
        next: (resp) => this.types = [{ id: 0, name: "All" }, ...resp],
        error: (e) => console.log(e)
      })
  }

  onBrandSelected(brandId: number) {
    const params = this.shopService.getShopParams();

    params.brandId = brandId;
    params.pageNumber = 1;

    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    const params = this.shopService.getShopParams();

    params.typeId = typeId;
    params.pageNumber = 1;

    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onSortSelected(sort: string) {
    const params = this.shopService.getShopParams();

    params.sort = sort;

    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onPageChanged(event: any) {
    if (this.shopParams.pageNumber !== event) {
      const params = this.shopService.getShopParams();

      params.pageNumber = event;

      this.shopService.setShopParams(params);
      this.getProducts(true);
    }
  }

  onSearch() {
    const params = this.shopService.getShopParams();

    params.search = this.searchTearm.nativeElement.value;
    params.pageNumber = 1;

    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onReset() {
    this.searchTearm.nativeElement.value = "";
    this.shopParams = new ShopParams();

    this.shopService.setShopParams(this.shopParams);
    this.getProducts();
  }
}