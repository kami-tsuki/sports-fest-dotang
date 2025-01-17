import { Injectable } from '@angular/core';
import {Team} from "@app/services/team.service";
import {User} from "@app/services/users.service";
import {EntityOfGuid} from "@app/services/api/sf-client";

export interface Discipline extends EntityOfGuid{
  name: string;
  teams?: Team[];
  judges?: User[];
}

@Injectable({
  providedIn: 'root'
})
export class DisciplinesService {
  private disciplines: Discipline[] = [];

  constructor() {}

  //// to get all disciplines
  getDisciplines(): Discipline[] {
    return this.disciplines;
  }

  //// to get specific discipline
  getDisciplineById(id: string): Discipline | undefined {
    return this.disciplines.find(discipline => discipline.id === id);
  }

  //// to add new discipline
  addDiscipline(newDiscipline: Discipline): void {
    this.disciplines.push(newDiscipline);
  }

  ///// to update adiscipline
  updateDiscipline(id: string, updatedDiscipline: Partial<Discipline>): void {
    const discipline = this.disciplines.find(d => d.id === id);
    if (discipline) {
      Object.assign(discipline, updatedDiscipline);
    }
  }

  //// to delete a discipline
  deleteDiscipline(id: string): void {
    this.disciplines = this.disciplines.filter(d => d.id !== id);
  }
}
