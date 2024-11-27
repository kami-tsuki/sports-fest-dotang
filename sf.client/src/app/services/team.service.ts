import { Injectable } from '@angular/core';

interface Team {
  id: number;
  name: string;
  members: number[]; // according tot sudent ID
  score?: number;
}

@Injectable({
  providedIn: 'root'
})
export class TeamsService {
  private teams: Team[] = [];

  constructor() {}

  //// to get all teams
  getTeams(): Team[] {
    return this.teams;
  }

  //// to get a specific team
  getTeamById(id: number): Team | undefined {
    return this.teams.find(team => team.id === id);
  }

  //// to add neu team
  addTeam(newTeam: Team): void {
    this.teams.push(newTeam);
  }

  //// to update ateam
  updateTeam(id: number, updatedTeam: Partial<Team>): void {
    const team = this.teams.find(t => t.id === id);
    if (team) {
      Object.assign(team, updatedTeam);
    }
  }

  //// to delete a team
  deleteTeam(id: number): void {
    this.teams = this.teams.filter(team => team.id !== id);
  }
}
