import { Component, OnInit } from '@angular/core';
import {Drink, Image} from "./drinks.model";
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
  public newPictute: Image;

  increaseForm : FormGroup;
  decreaseForm : FormGroup;
  costForm: FormGroup;
  imageForm: FormGroup;

  constructor(private drinksService: DrinksService){
    this.increaseForm = new FormGroup({
      count: new FormControl(''),
    });
    this.decreaseForm = new FormGroup({
      count: new FormControl(''),
    });
    this.costForm = new FormGroup({
      cost: new FormControl(''),
    });
    this.imageForm = new FormGroup({
      image: new FormControl(''),
    })
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
    this.costForm.setValue({cost: drink.cost});
    this.editableDrink = drink;
  }

  onFileChanged(event){
    this.drinksService.uploadFile(event.target.files[0]).subscribe(
      (data: Image) => {
        this.newPictute = data;
      },
      error => {
        console.error(error);
      }
    )
  }

  saveEditImage(drink:Drink){
    this.drinksService.update(drink).subscribe(
      () => {},
      error => {
        console.error(error);
      }
    );
  }

  onEditImage(event, drink:Drink){
    this.drinksService.uploadFile(event.target.files[0]).subscribe(
      (data: Image) => {
        drink.image = data;
      },
      error => {
        console.error(error);
      }
    )
  }

  addDrink(){
    this.newDrink.image = this.newPictute;
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

  updateDrink(drink:Drink){

    let cost = this.costForm.value['cost'];
    drink.cost = cost;

    this.drinksService.update(drink).subscribe(
      () => {},
      error => {
        console.error(error);
      }
    );
  }

  cancelEdit(){
    this.editableDrink = new Drink();
  }

  increase(drink: Drink){
    let count = this.increaseForm.value['count'];
    if (count > 0)
    {
      this.drinksService.increaseCount(drink, count).subscribe(
        () => {
          drink.count += count;
        },
        error => {
          console.error(error);
        }
      );
    }
  }

  decrease(drink: Drink){
    let count = this.decreaseForm.value['count'];
    if (count > 0) {
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
}
