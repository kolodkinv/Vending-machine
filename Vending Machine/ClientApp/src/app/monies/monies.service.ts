import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from "rxjs";
import {catchError} from "rxjs/operators";

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
}
