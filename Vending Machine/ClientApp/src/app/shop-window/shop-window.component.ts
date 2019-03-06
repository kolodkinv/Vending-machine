import { Component, OnInit } from '@angular/core';
import {ShopWindowService} from "./shop-window.service";
import {Order, Drink} from "./shop-window.model";
import {Money} from "../monies/monies.model";

@Component({
  selector: 'app-shop-window',
  templateUrl: './shop-window.component.html',
  styleUrls: ['./shop-window.component.css'],
  providers: [ShopWindowService]
})

export class ShopWindowComponent implements OnInit {
  order:Order;
  drinksInStore: Drink[];
  enableMoney: Money[];
  oddMoney: Money[];
  amount: number;

  constructor(private shopWindowService: ShopWindowService) { }

  ngOnInit() {
    this.loadDrinks();
    this.loadMoneis();
    this.order = new Order();
    this.oddMoney = [];
    this.amount = 0;
  }

  sell(){
    this.shopWindowService.sell(this.order).subscribe(
      (data:Money[]) => {
        this.order = new Order();
        this.oddMoney = data;
        this.amount = 0;
      },
      error => {
        console.error(error);
      }
    )
  }

  takeOddMoney(){
    this.oddMoney = [];
  }

  addDrinkToBasket(drink:Drink){
    if(this.order.oddMoney >= drink.cost){
      let currentDrink = Object.assign({}, drink);
      currentDrink.count = 1;
      let index = this.order.products.findIndex(d => d.id == drink.id);
      if (index > -1){
        this.order.products[index].count += currentDrink.count;
      } else {
        this.order.products.push(currentDrink);
      }
      this.order.oddMoney -= currentDrink.cost * currentDrink.count;
      this.order.amount += currentDrink.cost * currentDrink.count;
      drink.count -= currentDrink.count;
    }
  }

  addMoneyToBasket(money:Money){
    let currentMoney = Object.assign({}, money);
    currentMoney.count = 1;
    let index = this.order.money.findIndex(d => d.id == money.id);
    if (index > -1){
      this.order.money[index].count += currentMoney.count;
    } else {
      this.order.money.push(currentMoney);
    }
    this.order.oddMoney += currentMoney.cost * currentMoney.count;
    this.amount += currentMoney.cost * currentMoney.count;
  }

  loadDrinks(){
    this.shopWindowService.getAllDrinks().subscribe(
      (data:Drink[]) => {
        this.drinksInStore = data;
      },
      error => {
        console.error(error);
      }
    )
  }

  loadMoneis(){
    this.shopWindowService.getAllMoneis().subscribe(
      (data:Money[]) => {
        this.enableMoney = data;
      },
      error => {
        console.error(error);
      }
    )
  }

}
