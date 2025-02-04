import { Injectable } from '@angular/core';
import {User} from "@app/services/users.service";
import {EntityOfGuid} from "@app/services/api/sf-client";

export interface Class extends EntityOfGuid {
  name?: string;
  students?: User[];
  tutors?: User[];
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
  getClassById(id: string): Class | undefined {
    return this.classes.find(cls => cls.id === id);
  }

  //// to add new class
  addClass(newClass: Class): void {
    this.classes.push(newClass);
  }

  //// to update a class
  updateClass(id: string, updatedClass: Partial<Class>): void {
    const cls = this.classes.find(c => c.id === id);
    if (cls) {
      Object.assign(cls, updatedClass);
    }
  }

  //// to delete a class
  deleteClass(id: string): void {
    this.classes = this.classes.filter(cls => cls.id !== id);
  }
}
