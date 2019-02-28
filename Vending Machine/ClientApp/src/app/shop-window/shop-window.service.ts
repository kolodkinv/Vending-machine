import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {ItemInBasket} from "./shop-window.model";

@Injectable()
export class BasketService {

  urlDrinks: string = 'api/Drinks/';
  urlBasket: string = 'api/Baskets/';
  urlMoney: string = 'api/Money';

  constructor(private httpClient: HttpClient) {
  }

  public create(): Observable<any>{
    return this.httpClient.post(this.urlBasket, null);
  }

  public addDrink(drink:ItemInBasket): Observable<any>{
    return this.httpClient.put(this.urlBasket + 'AddProduct', drink);
  }

  public addMoney(drink:ItemInBasket): Observable<any>{
    return this.httpClient.put(this.urlBasket + 'AddMoney', drink);
  }

  public getAllDrinks(): Observable<any> {
    return this.httpClient.get(this.urlDrinks);
  }

  public getAllMoneis(): Observable<any> {
    return this.httpClient.get(this.urlMoney);
  }
}
