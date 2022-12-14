import { IAddress } from "./address";

export interface IOrderToCreate {
  basketId: string;
  deliveryMethodId: number;
  shippingAddress: IAddress;
}

export interface IOrder {
  id: number;
  buyerEmail: string;
  orderDate: string;
  shippingAddress: IAddress;
  deliveryMethod: string;
  orderItems: IOrderItem[];
  subtotal: number;
  total: string;
  status: number;
}

export interface IOrderItem {
  productId: number;
  productName: string;
  imageUrl: string;
  price: number;
  quantity: number;
}