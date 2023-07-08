import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Myservice } from '../myservice';
import { Employees } from '../IEmployee';
import { ActivatedRoute } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html'
  
})
export class EmployeeComponent implements OnInit {
  heroForm: FormGroup; // <--- heroForm is of type FormGroup
  employees: Employees[];
  id: any;
  localURL: string;
  localHttp: HttpClient;
  createForm() {
    this.heroForm = this.fb.group({
      name: ['', Validators.required],
      vacAccumulate: ['', [Validators.maxLength(2), Validators.required, Validators.pattern("^[0-9]*$")]],
      employeeType: ['', Validators.required],
      workDays: ['', Validators.required],
      id:['']
    });
  }
  constructor(private fb: FormBuilder, private myservice: Myservice,
    private activatedroute: ActivatedRoute, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.createForm();
    this.localURL = baseUrl;
    this.localHttp = http;
  }

  ngOnInit() {
    this.myservice.currentSource.subscribe(emp => {
      this.employees = emp;
      console.log(`Data Cahnged on Emp ${emp}`);
    });
    this.id = this.activatedroute.snapshot.paramMap.get('id');
    if (this.employees.length > 0) {
      let emp = this.employees.find(f => f.id == this.id);
      if(emp)
        this.heroForm.setValue({
          name: emp.name,
          vacAccumulate: emp.vacAccumalate,
          employeeType: emp.employeeType,
          workDays: emp.workDays,
          id: emp.id
      });
    }
   
  }


  public doSave() {
    let httpOptions={
      headers: new HttpHeaders({
        'Content-Type':'application/json'
      })
  };
    this.localHttp.post<Employees[]>(`${this.localURL}Employee`, this.heroForm.value, httpOptions).subscribe(result => {

      console.log(`data has been return ${result}`);

    });

  }

}
