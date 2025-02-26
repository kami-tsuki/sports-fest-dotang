import { Component } from '@angular/core';
import { TeamService, Team } from '../../services/team.service';
import { Discipline } from '../../services/discipline.service';


@Component({
  selector: 'app-top-class',
  templateUrl: './top-class.component.html',
  styleUrl: './top-class.component.css'
})
export class TopClassComponent {
  
  allDisciplines: Discipline[] = [
    {name: 'Discipline A', teams: [], judges: [], created: new Date(), updated: new Date(), init: () => {}, toJSON: () => ({}) },
    {name: 'Discipline B', teams: [], judges: [], created: new Date(), updated: new Date(), init: () => {}, toJSON: () => ({}) },
  ];

  teams: Team[] = [];
  result: Team[] = [];
  selectedDiscipline: Discipline | null = null;

  // to search among teams for the best records

  searchInTeam(): void {
    if (!this.selectedDiscipline) {
      this.result = [];
      return;
    }

    const filteredTeams = this.teams.filter(team => team.deciplien?.some(d => d.name === this.selectedDiscipline!.name));

    // Sort the filtered teams based on their points
    const sortedTeams = filteredTeams.sort((a, b) => b.point - a.point);

    // Select the top 10 teams
    this.result = sortedTeams.slice(0, 10);
  }

  // to get the top 10 teams in general
  getTop10Teams(): void {
    // Sort all teams based on their points
    const sortedTeams = this.teams.sort((a, b) => b.point - a.point);

    // Select the top 10 teams
    this.result = sortedTeams.slice(0, 10);
  }
}
