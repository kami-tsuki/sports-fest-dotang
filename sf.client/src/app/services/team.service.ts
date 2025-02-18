import {Injectable} from '@angular/core';
import {User} from './users.service';
import {EntityOfGuid} from "@app/services/api/sf-client";
import {Discipline} from "@app/services/discipline.service";
import {ApiService} from "@app/services/api.service";

export interface Team extends EntityOfGuid {
    name: string;
    students?: User[];
    fuhrer: string;
    user?: User[];
    discipline?: Discipline[] | null;
    point: number;
}

@Injectable({
    providedIn: 'root'
})
export class TeamService {

    constructor(
        private apiService: ApiService
    ) {
    }

    //// to get all teams
    getTeams(): Team[] {
        let teams: Team[] = [];
        this.apiService.get<Team[]>('teams').subscribe(t => {
            teams = t;
        });
        return teams;
    }

    //// to get a specific team
    getTeamById(id: string): Team | undefined {
        let team: Team | undefined;
        this.apiService.get<Team>(`teams/${id}`).subscribe(t => {
            team = t;
        });
        return team;
    }

    //// to add neu team
    addTeam(newTeam: Team): void {
        this.apiService.post<Team>('teams', newTeam).subscribe(t => {
            newTeam = t;
        });
    }

    //// to update ateam
    updateTeam(id: string, updatedTeam: Partial<Team>): void {
        this.apiService.put<Team>(`teams/${id}`, updatedTeam).subscribe(
            t => {
                updatedTeam = t;
            }
        );
    }

    //// to delete a team
    deleteTeam(id: string): void {
        this.apiService.delete<Team>(`teams/${id}`).subscribe();
    }
}
