import { Component, OnInit, ViewChild } from '@angular/core';
import { DisciplinesService , Discipline } from '../services/discipline.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { User, UsersService } from '@app/services/users.service';



@Component({
  selector: 'app-discipline-page',
  templateUrl: './discipline-page.component.html',
  styleUrl: './discipline-page.component.css'
})
export class DisciplinePageComponent {
  users: User[] = [];
  dess: Discipline[] = [];
  newDes: Discipline = {
    id: '',
    name: '',
    teams: [],
    judges: [],
    created: new Date(),
    updated: new Date(),
    init: () => {},
    toJSON: () => ({})
  };
  displayedColumns: string[] = ['id', 'name', 'teams', 'judges'];
  name: string = '';
  showSearchResults: boolean = false;
  showAddDesForm: boolean = false;
  searchName: string = '';
  selectedDes: Discipline | null = null;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  allDes = new MatTableDataSource<Discipline>([]);
  constructor(private desService: DisciplinesService, private usersService: UsersService) {}
  ngOnInit(): void {
    
  }
  addDes(): void {
    this.newDes.name = this.name;
    this.desService.addDiscipline(this.newDes);
  }
  searchDes(): void {
    const filteredDes = this.searchName
      ? this.dess.filter(des => des.name.includes(this.searchName))
      : this.dess;
      this.allDes.data = filteredDes;
      this.showSearchResults = !this.showSearchResults;
    }

  toggleAddDesForm(): void {
    this.showAddDesForm = !this.showAddDesForm;
  }

  onCheckboxChange(des: Discipline, event: Event): void {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.selectedDes = isChecked
      ? {
          ...des,
          init: des.init || (() => {}),
          toJSON: des.toJSON || (() => ({})),
        }
      : null;
  }

  saveDes(): void {
    if (this.selectedDes) {
      if (this.selectedDes && this.selectedDes.id) {
        this.desService.updateDiscipline(this.selectedDes.id, this.selectedDes);
      }
    }
  }

  deleteDes(): void {
    if (this.selectedDes && this.selectedDes.id) {
      this.desService.deleteDiscipline(this.selectedDes.id);
    }
  }



}
