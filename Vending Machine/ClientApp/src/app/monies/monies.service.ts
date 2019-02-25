import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from "rxjs";
import {catchError} from "rxjs/operators";
import {Money} from "./monies.model";

@Injectable()
export class MoniesService {

  url: string = 'api/Money/';

  constructor(private httpClient: HttpClient) { }

  public getAll(): Observable<any>{
    return this.httpClient.get(this.url);
  }

  public increaseCount(money:Money, count:number): Observable<any>{
    let currentMoney = Object.assign({}, money);
    currentMoney.count = count;
    return this.httpClient.put(this.url + 'Increase', currentMoney);
  }

  public decreaseCount(money:Money, count:number): Observable<any>{
    let currentMoney = Object.assign({}, money);
    currentMoney.count = count;
    return this.httpClient.put(this.url + 'Decrease', currentMoney);
  }

  public update(money:Money): Observable<any>{
    return this.httpClient.put(this.url, money);
  }

  public create(money:Money): Observable<any>{
    return this.httpClient.post(this.url, money);
  }

  public changeEnable(money:Money): Observable<any>{
    let currentMoney = Object.assign({}, money);
    currentMoney.enable = !currentMoney.enable;
    return this.httpClient.put(this.url, currentMoney);
  }
}
