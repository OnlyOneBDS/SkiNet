import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { faAngleLeft, faAngleRight } from '@fortawesome/free-solid-svg-icons';
import { BasketService } from 'src/app/basket/basket.service';

import { IDeliveryMethod } from 'src/app/shared/models/deliveryMethod';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss']
})
export class CheckoutDeliveryComponent implements OnInit {
  faAngleLeft = faAngleLeft;
  faAngleRight = faAngleRight;
  @Input() checkoutForm: FormGroup;
  deliveryMethods: IDeliveryMethod[];

  constructor(private checkoutService: CheckoutService, private basketService: BasketService) { }

  ngOnInit(): void {
    this.checkoutService
      .getDeliveryMethods()
      .subscribe({
        next: (dm: IDeliveryMethod[]) => this.deliveryMethods = dm,
        error: (e) => console.log(e)
      })
  }

  setShippingPrice(deliveryMethod: IDeliveryMethod) {
    this.basketService.setShippingPrice(deliveryMethod);
  }
}
