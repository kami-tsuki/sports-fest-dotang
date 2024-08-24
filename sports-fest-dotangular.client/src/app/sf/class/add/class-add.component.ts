import {Component, OnInit} from '@angular/core';
import {Class, ResultModelOfClass} from '../../../services/api/sf-client';
import {HttpClient} from '@angular/common/http';
import {AppPaths} from "../../../app-paths";

@Component({
  selector: 'sf-class-add',
  templateUrl: './class-add.component.html',
  styleUrls: ['./class-add.component.css']
})
export class ClassAddComponent implements OnInit {

  constructor(private http: HttpClient) {
  }

  public class: Class = new Class();

  ngOnInit() {
    console.log('ClassAddComponent initialized');
  }

  createClass(cl: Class) {
    this.http.post('/api/v1/data/classes', cl).subscribe(
      (r) => {
        let result = r as ResultModelOfClass;
        console.log(result);
        if (result && result.success) {
          console.log('Class created successfully');
          window.location.href = AppPaths.classBase + AppPaths.list;
        } else {
          console.error('Error creating class: ' + result.message, result);
        }
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'Sports Fest | Add Class';
}
