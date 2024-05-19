
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Program , GetProgramsResponse } from '../program';
import { ProgramService } from '../program.service';

@Component({
  selector: 'program-cancel',
  standalone: true,
  imports: [],
  templateUrl: './cancel.component.html',
  styleUrl: './cancel.component.css'
})

export class ProgramCancelComponent {

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

  cancel(): void {
    this.programService.cancel(this.id,).subscribe((res:any) => {
      console.log('Program canceled successfully!');
      this.router.navigateByUrl('program/list');
    });
  }
}
