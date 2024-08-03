import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProgramService } from '../program.service';
import { Router } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'program-add',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add.component.html',
  styleUrl: './add.component.css'
})
export class ProgramAddComponent {

  form!: FormGroup;
  saved: boolean = false;

  constructor(
   public programService: ProgramService,
    private router: Router
  ) { }
    
  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl('', [Validators.required]),
      description:  new FormControl(''),
      stateId:  new FormControl('', [Validators.required]),
      startDate:  new FormControl('',[Validators.required]),
      endDate:  new FormControl('',[Validators.required])
    });
  }

  get f(){
    return this.form.controls;
  }

  submit(){
    console.log(this.form.value);
    this.programService.create(this.form.value).subscribe((res:any) => {
         console.log('Post created successfully!');
         this.saved = true;
         this.router.navigateByUrl('program/list');
    })
  }
}

