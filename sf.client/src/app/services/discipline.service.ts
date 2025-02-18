import {Injectable} from '@angular/core';
import {Team} from "@app/services/team.service";
import {User} from "@app/services/users.service";
import {EntityOfGuid} from "@app/services/api/sf-client";
import {ApiService} from "@app/services/api.service";

export interface Discipline extends EntityOfGuid {
    name: string;
    teams?: Team[];
    judges?: User[];
}

@Injectable({
    providedIn: 'root'
})
export class DisciplinesService {

    constructor(
        private apiService: ApiService
    ) {
    }

    //// to get all disciplines
    getDisciplines(): Discipline[] {
        let disciplines: Discipline[] = [];
        this.apiService.get<Discipline[]>('discipline').subscribe(d => {
            disciplines = d;
        });
        return disciplines;
    }

    //// to get specific discipline
    getDisciplineById(id: string): Discipline | undefined {
        let discipline: Discipline | undefined;
        this.apiService.get<Discipline>(`discipline/${id}`).subscribe(d => {
            discipline = d;
        });
        return discipline;
    }

    //// to add new discipline
    addDiscipline(newDiscipline: Discipline): void {
        this.apiService.post<Discipline>('discipline', newDiscipline).subscribe(
            d => {
                newDiscipline = d;
            }
        );
    }

    ///// to update a discipline
    updateDiscipline(id: string, updatedDiscipline: Partial<Discipline>): void {
        this.apiService.put<Discipline>(`discipline/${id}`, updatedDiscipline).subscribe(
            d => {
                updatedDiscipline = d;
            }
        );
    }

    //// to delete a discipline 
    deleteDiscipline(id: string): void {
        this.apiService.delete(`discipline/${id}`).subscribe();
    }
}
