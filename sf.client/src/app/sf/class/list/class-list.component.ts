import { Component, OnInit } from '@angular/core';
import {Class, ResultModelOfPageOfClass} from "../../../services/api/sf-client";
import {HttpClient} from "@angular/common/http";
import {AppPaths} from "../../../app-paths";

@Component({
  selector: 'sf-class-list',
  templateUrl: './class-list.component.html',
  styleUrls: ['./class-list.component.css']
})
export class ClassListComponent implements OnInit {
  public classes: Class[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    console.log('ListComponent initialized');
    this.getClasses();
  }

  getClasses() {
    this.http.get<ResultModelOfPageOfClass>('/api/v1/data/classes').subscribe(
      (result) => {
        this.classes = result.data?.data || [];
        console.log(this.classes);
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'Sports Fest | Classes';
  protected readonly AppPaths = AppPaths;
}
