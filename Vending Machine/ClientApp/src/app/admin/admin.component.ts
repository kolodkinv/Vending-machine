import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  password:string;
  private querySubscription: Subscription;

  constructor(private route: ActivatedRoute) {
    this.querySubscription = route.queryParams.subscribe(
      (queryParam: any) => {
        this.password = queryParam['password'];
        if (this.password != 'admin'){
          this.password = '';
        }
      }
    );
  }

  ngOnInit() {
  }

}
