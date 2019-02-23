import {Component, Inject, OnInit} from '@angular/core';
import {MoniesService} from "./monies.service";
import {FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule} from "@angular/forms";


@Component({
  selector: 'app-monies',
  templateUrl: './monies.component.html',
  styleUrls: ['./monies.component.css'],
  providers: [MoniesService]
})
export class MoniesComponent implements OnInit{

  public monies: Money[];

  increaseForm : FormGroup;

  constructor(private moniesService: MoniesService){
    this.increaseForm = new FormGroup({
      count: new FormControl(''),
    });
  }

  ngOnInit(){

    this.moniesService.getAll().subscribe(
      (data:Money[]) => {
        this.monies = data
      },
      error => {
        // Ошибка
      })
  }

  increase(money: Money){
    let count = this.increaseForm.value['count'];
    this.moniesService.increaseCount(money, count).subscribe(
      () => {
        //this.monies[index].count += count;
        debugger;
        money.count += count;
      },
      error => {

      }
    );
  }


}
/*
interface Money {
  id: number;
  name: string;
  count: number;
  cost: number;
  enabled: boolean;
}*/
