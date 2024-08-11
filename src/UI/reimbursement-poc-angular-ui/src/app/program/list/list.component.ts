import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Program , GetProgramsResponse } from '../program';
import { ProgramService } from '../program.service';

@Component({
  selector: 'program-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './list.component.html',
  styleUrl: './list.component.css'
})
export class ProgramListComponent {

  programs: Program[] = [];

  constructor(public programService: ProgramService) { }
    
  ngOnInit(): void {
    this.programService.getAll().subscribe((data: GetProgramsResponse)=>{
      this.programs = data.items;
      console.log(this.programs);
    })  
  }
}