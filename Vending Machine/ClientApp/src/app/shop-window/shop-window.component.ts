import { Component, OnInit } from '@angular/core';
import {BasketService} from "./shop-window.service";
import {Basket, Drink} from "./shop-window.model";
import {Money} from "../monies/monies.model";

@Component({
  selector: 'app-shop-window',
  templateUrl: './shop-window.component.html',
  styleUrls: ['./shop-window.component.css'],
  providers: [BasketService]
})
export class ShopWindowComponent implements OnInit {
  basket:Basket;
  drinksInStore: Drink[];
  moneyEnableMoney: Money[];

  constructor(private basketService: BasketService) { }

  ngOnInit() {
    this.loadDrinks();
    this.loadMoneis();
    this.basket = new Basket();
  }

  addDrinkToBasket(drink:Drink){
    if(this.basket.oddMoney > drink.cost){
      let currentDrink = Object.assign({}, drink);
      currentDrink.count = 1;
      let index = this.basket.drinks.findIndex(d => d.id == drink.id);
      if (index > -1){
        this.basket.drinks[index].count += currentDrink.count;
      } else {
        this.basket.drinks.push(currentDrink);
      }

      this.basket.oddMoney -= currentDrink.cost * currentDrink.count;
      this.basket.amount += currentDrink.cost * currentDrink.count;
    }
  }

  addMoneyToBasket(money:Money){
    let currentMoney = Object.assign({}, money);
    currentMoney.count = 1;
    let index = this.basket.money.findIndex(d => d.id == money.id);
    if (index > -1){
      this.basket.money[index].count += currentMoney.count;
    } else {
      this.basket.money.push(currentMoney);
    }
    this.basket.oddMoney += currentMoney.cost * currentMoney.count;
  }

  loadDrinks(){
    this.basketService.getAllDrinks().subscribe(
      (data:Drink[]) => {
        this.drinksInStore = data;
      },
      error => {
        console.error(error);
      }
    )
  }

  loadMoneis(){
    this.basketService.getAllMoneis().subscribe(
      (data:Money[]) => {
        this.moneyEnableMoney = data;
      },
      error => {
        console.error(error);
      }
    )
  }

}
