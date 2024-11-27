import { Injectable } from '@angular/core';

interface Class {
  id: number;
  name: string;
  students: number[]; // this is for student IDs
  totalScore?: number; // this is to calculate score based on students' points
}

@Injectable({
  providedIn: 'root'
})
export class ClassesService {
  private classes: Class[] = [];

  constructor() {}

  //// to get all classes
  getClasses(): Class[] {
    return this.classes;
  }

  //// to get specific class by id
  getClassById(id: number): Class | undefined {
    return this.classes.find(cls => cls.id === id);
  }

  //// to add new class
  addClass(newClass: Class): void {
    this.classes.push(newClass);
  }

  //// to update a class
  updateClass(id: number, updatedClass: Partial<Class>): void {
    const cls = this.classes.find(c => c.id === id);
    if (cls) {
      Object.assign(cls, updatedClass);
    }
  }

  //// to delete a class
  deleteClass(id: number): void {
    this.classes = this.classes.filter(cls => cls.id !== id);
  }
}
