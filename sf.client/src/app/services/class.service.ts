import {Injectable} from '@angular/core';
import {User} from "@app/services/users.service";
import {EntityOfGuid} from "@app/services/api/sf-client";
import {ApiService} from "@app/services/api.service";

export interface Class extends EntityOfGuid {
    name?: string;
    students?: User[];
    tutors?: User[];
}

@Injectable({
    providedIn: 'root'
})
export class ClassesService {

    constructor(
        private apiService: ApiService
    ) {
    }

    //// to get all classes
    getClasses(): Class[] {
        let classes: Class[] = [];
        this.apiService.get<Class[]>('classes').subscribe(c => {
            classes = c;
        });
        return classes;
    }

    //// to get specific class by id
    getClassById(id: string): Class | undefined {
        let cls: Class | undefined;
        this.apiService.get<Class>(`classes/${id}`).subscribe(c => {
            cls = c;
        });
        return cls;
    }

    //// to add new class
    addClass(newClass: Class): void {
        this.apiService.post<Class>('classes', newClass).subscribe(c => {
            newClass = c;
        });
    }

    //// to update a class
    updateClass(id: string, updatedClass: Partial<Class>): void {
        this.apiService.put<Class>(`classes/${id}`, updatedClass).subscribe(
            c => {
                updatedClass = c;
            }
        );
    }

    //// to delete a class
    deleteClass(id: string): void {
        this.apiService.delete(`classes/${id}`).subscribe();
    }
}
