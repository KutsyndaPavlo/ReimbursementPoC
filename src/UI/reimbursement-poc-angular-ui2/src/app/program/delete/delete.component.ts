
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { Program , GetProgramsResponse } from '../program';
import { ProgramService } from '../program.service';

@Component({
  selector: 'program-cancel',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './delete.component.html',
  styleUrl: './delete.component.css'
})

export class ProgramDeleteComponent {

  id!: number;
  program!: Program;
    
  constructor(
    public programService: ProgramService,
    private route: ActivatedRoute,
    private router: Router
   ) { }
    

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
        
    this.programService.find(this.id).subscribe((data: Program)=>{
      this.program = data;
    });
  }

  delete(): void {
    this.programService.delete(this.id,).subscribe((res:any) => {
      console.log('Program deleted successfully!');
      this.router.navigateByUrl('program/list');
    });
  }
}
