import {Money} from "../monies/monies.model";

export class Drink {
  id: number;
  name: string;
  count: number;
  cost: number;
  image: Image;

  constructor(){
  }
}

export class Image {
  id: number;
  normalImage: string;
}

export class ItemInBasket {
  id:number;
  count:number;
  idBasket:number;
}

export class Order {
  id: number;
  products: Drink[];
  money: Money[];
  amount: number;
  oddMoney: number;

  constructor(){
    this.id = 0;
    this.products = [];
    this.money = [];
    this.amount = 0;
    this.oddMoney = 0;
  }
}
