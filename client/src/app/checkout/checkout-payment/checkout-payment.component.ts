import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { faAngleLeft, faAngleRight } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';

import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { IOrder } from 'src/app/shared/models/order';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements OnInit {
  faAngleLeft = faAngleLeft;
  faAngleRight = faAngleRight;
  @Input() checkoutForm: FormGroup;

  constructor(private basketService: BasketService, private checkoutService: CheckoutService, private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
  }

  submitOrder() {
    const basket = this.basketService.getCurrentBasketValue();
    const orderToCreate = this.getOrderToCreate(basket);

    this.checkoutService.createOrder(orderToCreate).subscribe({
      next: (order: IOrder) => {
        const navigationExtras: NavigationExtras = { state: order };

        this.toastr.success("Order created successfully");
        this.basketService.deleteLocalBasket(basket.id);
        this.router.navigate(["checkout/success"], navigationExtras);
        console.log(order);
      },
      error: (e) => {
        this.toastr.error(e.message);
        console.log(e);
      }
    });
  }

  private getOrderToCreate(basket: IBasket) {
    return {
      basketId: basket.id,
      deliveryMethodId: +this.checkoutForm.get("deliveryForm").get("deliveryMethod").value,
      shippingAddress: this.checkoutForm.get("addressForm").value
    }
  }
}
