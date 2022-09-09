import { v4 as uuidv4 } from 'uuid';

export interface IBasket {
  id: string;
  items: IBasketItem[];
}

export interface IBasketItem {
  id: number;
  productName: string;
  price: number;
  quantity: number;
  imageUrl: string;
  brand: string;
  type: string;
}

export interface IBasketTotals {
  shipping: number;
  subTotal: number;
  total: number;
}

export class Basket implements IBasket {
  id = uuidv4();
  items: IBasketItem[] = [];
}