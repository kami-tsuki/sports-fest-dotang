import { Injectable } from '@angular/core';
import { User } from './users.service';
import {EntityOfGuid} from "@app/services/api/sf-client";
import {Discipline} from "@app/services/discipline.service";

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
  private teams: Team[] = []; 

  constructor() {}

  //// to get all teams
  getTeams(): Team[] {
    return this.teams;
  }

  //// to get a specific team
  getTeamById(id: string): Team | undefined {
    return this.teams.find(team => team.id === id);
  }

  //// to add neu team
  addTeam(newTeam: Team): void {
    this.teams.push(newTeam);
  }

  //// to update ateam
  updateTeam(id: string, updatedTeam: Partial<Team>): void {
    const team = this.teams.find(t => t.id === id);
    if (team) {
      Object.assign(team, updatedTeam);
    }
  }

  //// to delete a team
  deleteTeam(id: string): void {
    this.teams = this.teams.filter(team => team.id !== id);
  }
}
