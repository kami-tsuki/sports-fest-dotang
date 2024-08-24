import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {Class} from "./services/api/sf-client";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  ngOnInit() {
    console.log('AppComponent initialized');
  }


  title = 'Sports Fest | Home';
}
