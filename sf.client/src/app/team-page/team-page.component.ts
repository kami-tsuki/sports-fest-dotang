import { Component } from '@angular/core';
import { TeamService, Team } from '../services/team.service';
import { Discipline } from '../services/discipline.service';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-team-page',
  templateUrl: './team-page.component.html',
  styleUrls: ['./team-page.component.css']
})

export class TeamPageComponent {
  
  name: string = '';
  showSearchResults: boolean = false;
  showAddTeamForm: boolean = false;
  selectedTeam: Team | null = null;
  searchName: string = '';
  selectedDiscipline: Discipline | null = null;

  displayedColumns: string[] = ['select', 'id', 'name', 'fuhrer', 'deciplien', 'point'];
  allTeam = new MatTableDataSource<Team>([]);
  teams: Team[] = [];

  newTeam: Team = this.resetNewTeam();
  

  allDisciplines: Discipline[] = [
    { id: '1', name: 'Discipline A', teams: [], judges: [], created: new Date(), updated: new Date(), init: () => {}, toJSON: () => ({}) },
    { id: '2', name: 'Discipline B', teams: [], judges: [], created: new Date(), updated: new Date(), init: () => {}, toJSON: () => ({}) },
  ];

  constructor(private teamService: TeamService) {}

  // Reset the new team object
  private resetNewTeam(): Team {
    return {
      id: '',
      name: '',
      fuhrer: '',
      point: 0,
      created: new Date(),
      updated: new Date(),
      init: () => {},
      toJSON: () => ({})
    };
  }

  // Search Teams
  searchTeam(): void {
    const filteredTeams = this.searchName
      ? this.teams.filter(team => team.name.includes(this.searchName))
      : this.teams;

    this.allTeam.data = filteredTeams;
    this.showSearchResults = !this.showSearchResults;
  }

  // Toggle add team form visibility
  toggleAddTeamForm(): void {
    this.showAddTeamForm = !this.showAddTeamForm;
  }

  // Select a team for editing
  onCheckboxChange(team: Team, event: Event): void {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.selectedTeam = isChecked
      ? {
          ...team,
          init: team.init || (() => {}),
          toJSON: team.toJSON || (() => ({})),
        }
      : null;
  }
  

  // Save team after editing
  saveTeam(): void {
    if (this.selectedTeam && this.selectedTeam.id) {
      /// Replace the discipline array with the selected discipline (if any)
      this.selectedTeam.discipline = this.selectedDiscipline ? [this.selectedDiscipline] : [];


      this.teamService.updateTeam(this.selectedTeam.id, this.selectedTeam);
      this.refreshTeams();
      console.log('Team updated successfully:', this.selectedTeam);
      alert('Team updated successfully!');
      this.selectedTeam = null;
      this.selectedDiscipline = null;
    } else {
      console.error('Cannot update team: ID is missing.');
      alert('Cannot update team: ID is missing.');
    }
  }

  // Delete team
  deleteTeam(): void {
    if (this.selectedTeam && this.selectedTeam.id) {
      this.teamService.deleteTeam(this.selectedTeam.id);
      this.refreshTeams();
      console.log('Team deleted successfully:', this.selectedTeam);
      alert('Team deleted successfully!');
      this.selectedTeam = null;
    } else {
      console.error('Cannot delete team: ID is missing.');
      alert('Cannot delete team: ID is missing.');
    }
  }

  // Add new team
  addTeam(): void {
    try {
      //// Set the new team's discipline array to the selected discipline (if any)
      this.newTeam.discipline = this.selectedDiscipline ? [this.selectedDiscipline] : [];


      this.teamService.addTeam(this.newTeam);
      this.refreshTeams();

      console.log('Team added successfully', this.teams);
      alert('Team added successfully!');
      this.newTeam = this.resetNewTeam();
      this.showAddTeamForm = false;
      this.selectedDiscipline = null;
    } catch (error) {
      console.error('Error adding team:', error);
      alert('Failed to add team. Please try again.');
    }
  }

  // Get discipline names for display in the table
  getDisciplineNames(team: Team): string {
    return team.deciplien && team.deciplien.length > 0
      ? team.deciplien.map(d => d.name).join(', ')
      : 'None';
  }


  // Refresh the list of teams and update the data table
  private refreshTeams(): void {
    this.teams = this.teamService.getTeams();
    this.allTeam.data = [...this.teams];
  }
}
