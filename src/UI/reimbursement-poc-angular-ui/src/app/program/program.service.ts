
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HTTP_INTERCEPTORS } from '@angular/common/http';
     
import {  Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
  
import { CreateProgram, UpdateProgram } from './program';
  
@Injectable({
  providedIn: 'root'
})
export class ProgramService {
  
private apiURL = "http://localhost:8501";
    
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }
   
  constructor(private httpClient: HttpClient) { }
    
  getAll(): Observable<any> {
  
    return this.httpClient.get("assets/data/get_programs.json") //this.apiURL + '/api/programs')
  
    .pipe(
      catchError(this.errorHandler)
    )
  }
    
  create(post:CreateProgram): Observable<any> {
  
    return this.httpClient.post(this.apiURL + '/api/programs', JSON.stringify(post), this.httpOptions)
  
    .pipe(
      catchError(this.errorHandler)
    )
  }  
    

  find(id:number): Observable<any> {
  
    return this.httpClient.get("assets/data/get_by_id.json")//this.apiURL + '/api/programs/' + id)
  
    .pipe(
      catchError(this.errorHandler)
    )
  }
    
   update(id:number, program: UpdateProgram): Observable<any> {
  
    return this.httpClient.put(this.apiURL + '/api/programs/' + id, JSON.stringify(program), this.httpOptions)
 
    .pipe( 
      catchError(this.errorHandler)
    )
  }

  cancel(id:number): Observable<any> {
  
    return this.httpClient.put(this.apiURL + '/api/programs/' + id + '/cancel', this.httpOptions)
 
    .pipe( 
      catchError(this.errorHandler)
    )
  }
       
  delete(id:number){
    return this.httpClient.delete(this.apiURL + '/api/programs/' + id, this.httpOptions)
  
    .pipe(
      catchError(this.errorHandler)
    )
  }
      
  errorHandler(error:any) {
    let errorMessage = '';
    if(error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(errorMessage);
 }
}