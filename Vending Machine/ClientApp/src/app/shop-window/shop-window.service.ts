import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Order} from "./shop-window.model";

@Injectable()
export class ShopWindowService {

  urlDrinks: string = 'api/Drinks/';
  urlOrders: string = 'api/Orders/';
  urlMoney: string = 'api/Money';

  constructor(private httpClient: HttpClient) {
  }

  public sell(order: Order): Observable<any> {
    return this.httpClient.post(this.urlOrders, order);
  }

  public getAllDrinks(): Observable<any> {
    return this.httpClient.get(this.urlDrinks);
  }

  public getAllMoneis(): Observable<any> {
    return this.httpClient.get(this.urlMoney);
  }
}
