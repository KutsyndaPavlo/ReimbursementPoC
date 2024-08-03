import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { Program , GetProgramsResponse } from '../program';
import { ProgramService } from '../program.service';

@Component({
  selector: 'program-edit',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.css'
})
export class ProgramEditComponent {

  id!: number;
  program!: Program;
  form!: FormGroup;
    
  constructor(
    public programService: ProgramService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.programService.find(this.id).subscribe((data: Program)=>{
      console.log(data);
      this.program = data;
    }); 
      
    this.form = new FormGroup({
      id: new FormControl(''),
      name: new FormControl('', [Validators.required]),
      description: new FormControl(''),
      lastModified: new FormControl('')
    });
  }
    
  get f(){
    return this.form.controls;
  }
    
  submit(){
    console.log(this.form.value);
    this.programService.update(this.id, this.form.value).subscribe((res:any) => {
         console.log('Program updated successfully!');
         this.router.navigateByUrl('program/list');
    })
  }

}

