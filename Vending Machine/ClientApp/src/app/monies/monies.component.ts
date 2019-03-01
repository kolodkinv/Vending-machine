import {Component, Inject, OnInit} from '@angular/core';
import {MoniesService} from "./monies.service";
import {FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {Money} from "./monies.model";



@Component({
  selector: 'app-monies',
  templateUrl: './monies.component.html',
  styleUrls: ['./monies.component.css'],
  providers: [MoniesService]
})
export class MoniesComponent implements OnInit{

  public monies: Money[];
  public currentMoney: Money = new Money();
  public newMoney: Money = new Money();

  increaseForm : FormGroup;
  decreaseForm : FormGroup;

  constructor(private moniesService: MoniesService){
    this.increaseForm = new FormGroup({
      count: new FormControl(''),
    });
    this.decreaseForm = new FormGroup({
      count: new FormControl(''),
    });
  }

  ngOnInit(){
    this.loadMoneis();
  }

  loadMoneis(){
    this.moniesService.getAll().subscribe(
      (data:Money[]) => {
        this.monies = data
      },
      error => {
        console.error(error);
      })
  }

  editMoney(money: Money){
    this.increaseForm.setValue({count: 0});
    this.decreaseForm.setValue({count: 0});
    this.currentMoney = money;
  }

  addMoney(){
    this.moniesService.create(this.newMoney).subscribe(
      (data: Money) => {
        this.monies.push(data);
      },
      error => {
        debugger;
        console.error(error);
      }
    );
    this.newMoney = new Money();
  }

  cancelEdit(){
    this.currentMoney = new Money();
  }

  changeEnable(money: Money){
    this.moniesService.changeEnable(money).subscribe(
      () => {
        money.enable = !money.enable;
      },
      error => {
        console.error(error);
      }
    )
  }

  increase(money: Money){
    let count = this.increaseForm.value['count'];
    this.moniesService.increaseCount(money, count).subscribe(
      () => {
        money.count += count;
      },
      error => {
        console.error(error);
      }
    );
  }

  decrease(money: Money){
    let count = this.decreaseForm.value['count'];
    this.moniesService.decreaseCount(money, count).subscribe(
      () => {
        money.count -= count;
      },
      error => {
        console.error(error);
      }
    );
  }
}

