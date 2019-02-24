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

  public monies: Money[];             // Список всех денег
  public currentMoney: Money = new Money();  // Редактируемые деньги

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
    this.moniesService.getAll().subscribe(
      (data:Money[]) => {
        this.monies = data
      },
      error => {
        // TODO Ошибка
      })
  }

  editMoney(money: Money){
    debugger;
    this.increaseForm.setValue({count: 0});
    this.decreaseForm.setValue({count: 0});
    this.currentMoney = money;
  }

  cancelEdit(){
    this.currentMoney = new Money();
  }

  increase(money: Money){
    let count = this.increaseForm.value['count'];
    this.moniesService.increaseCount(money, count).subscribe(
      () => {
        money.count += count;
      },
      error => {
        // TODO Ошибка
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
        // TODO Ошибка
      }
    );
  }
}

