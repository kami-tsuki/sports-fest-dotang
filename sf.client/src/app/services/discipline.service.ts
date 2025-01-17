import { Injectable } from '@angular/core';

interface Discipline {
  id: number;
  name: string;
  teamsNumber: number
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
  getDisciplineById(id: number): Discipline | undefined {
    return this.disciplines.find(discipline => discipline.id === id);
  }

  //// to add new discipline
  addDiscipline(newDiscipline: Discipline): void {
    this.disciplines.push(newDiscipline);
  }

  ///// to update adiscipline
  updateDiscipline(id: number, updatedDiscipline: Partial<Discipline>): void {
    const discipline = this.disciplines.find(d => d.id === id);
    if (discipline) {
      Object.assign(discipline, updatedDiscipline);
    }
  }

  //// to delete a discipline
  deleteDiscipline(id: number): void {
    this.disciplines = this.disciplines.filter(d => d.id !== id);
  }
}
