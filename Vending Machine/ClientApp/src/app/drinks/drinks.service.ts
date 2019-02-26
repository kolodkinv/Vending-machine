import {Drink} from "./drinks.model";
import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Money} from "../monies/monies.model";

@Injectable()
export class DrinksService {

  url: string = 'api/Drinks/';

  constructor(private httpClient: HttpClient) {
  }

  public getAll(): Observable<any> {
    return this.httpClient.get(this.url);
  }

  public create(drink:Drink): Observable<any>{
    return this.httpClient.post(this.url, drink);
  }

  public update(drink:Drink): Observable<any>{
    return this.httpClient.put(this.url, drink);
  }

  public increaseCount(drink:Drink, count:number): Observable<any>{
    let currentDrink = Object.assign({}, drink);
    currentDrink.count += count;
    return this.httpClient.put(this.url, currentDrink);
  }

  public decreaseCount(drink:Drink, count:number): Observable<any>{
    let currentDrink = Object.assign({}, drink);
    currentDrink.count -= count;
    return this.httpClient.put(this.url, currentDrink);
  }
}
