import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'; 

@Component({
  selector: 'app-manger-component',
  templateUrl: './manger-component.component.html',
  styleUrls: ['./manger-component.component.css']
})
export class MangerComponentComponent {
  //ToDo-->following data are required
  user = {
    firstName: '',
    lastName: '',
  }; 

  
}
