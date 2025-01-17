import { Component } from '@angular/core';

@Component({
  selector: 'app-teacher-page',
  templateUrl: './teacher-page.component.html',
  styleUrl: './teacher-page.component.css'
})
export class TeacherPageComponent {
    //ToDo-->following data are required
    user = {
      firstName: '',
      lastName: '',
      klass: '',
      group: ''

    }; 

}
