import { Component, OnInit } from '@angular/core';
import {Drink} from "./drinks.model";
import {FormControl, FormGroup} from "@angular/forms";
import {DrinksService} from "./drinks.service";
import {Money} from "../monies/monies.model";

@Component({
  selector: 'app-drinks',
  templateUrl: './drinks.component.html',
  styleUrls: ['./drinks.component.css'],
  providers: [DrinksService]
})
export class DrinksComponent implements OnInit {

  public drinks: Drink[];
  public editableDrink: Drink = new Drink();
  public newDrink: Drink = new Drink();

  increaseForm : FormGroup;
  decreaseForm : FormGroup;

  constructor(private drinksService: DrinksService){
    this.increaseForm = new FormGroup({
      count: new FormControl(''),
    });
    this.decreaseForm = new FormGroup({
      count: new FormControl(''),
    });
  }

  ngOnInit() {
    this.loadDrinks()
  }

  loadDrinks(){
    this.drinksService.getAll().subscribe(
      (data:Drink[]) => {
        this.drinks = data
      },
      error => {
        console.error(error);
      }
    )
  }

  editDrink(drink: Drink){
    this.increaseForm.setValue({count: 0});
    this.decreaseForm.setValue({count: 0});
    this.editableDrink = drink;
  }

  onFileChanged(event) {
    this.drinksService.uploadFile(event.target.files[0]).subscribe(
      () => {},
      error => {
        console.error(error);
      }
    )
  }

  addDrink(){
    this.drinksService.create(this.newDrink).subscribe(
      (data: Drink) => {
        this.drinks.push(data);
      },
      error => {
        console.error(error);
      }
    );
    this.newDrink = new Drink();
  }

  updateDrink(){
    this.drinksService.update(this.newDrink).subscribe(
      () => {
        this.loadDrinks();
      },
      error => {
        console.error(error);
      }
    )
  }

  cancelEdit(){
    this.editableDrink = new Drink();
  }

  increase(drink: Drink){
    let count = this.increaseForm.value['count'];
    this.drinksService.increaseCount(drink, count).subscribe(
      () => {
        drink.count += count;
      },
      error => {
        console.error(error);
      }
    );
  }

  decrease(drink: Drink){
    let count = this.decreaseForm.value['count'];
    this.drinksService.decreaseCount(drink, count).subscribe(
      () => {
        drink.count -= count;
      },
      error => {
        console.error(error);
      }
    );
  }
}
