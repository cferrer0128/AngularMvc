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
  empForm: FormGroup; // <--- empForm is of type FormGroup
  employees: Employees[];
  id: any;
  localURL: string;
  localHttp: HttpClient;
  localEmp: Employees;
  createForm() {
    this.empForm = this.fb.group({
      name: ['', Validators.required],
      vacAccumulate: ['', [Validators.maxLength(2), Validators.required, Validators.pattern("^[0-9]*$")]],
      employeeType: ['', Validators.required],
      workDays: ['', [Validators.maxLength(3), Validators.required, Validators.pattern("^[0-9]*$")]],
      id: [''],
      modified: [''],
      vacDays: ['', [Validators.maxLength(2), Validators.required, Validators.pattern("^[0-9]*$")]]
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
      let localemp = this.employees.find(f => f.id == this.id);
      if (localemp)
        this.empForm.setValue({
          name: localemp.name,
          vacAccumulate: localemp.vacAccumulate,
          employeeType: localemp.employeeType,
          workDays: localemp.workDays,
          id: localemp.id,
          modified: localemp.modified,
          vacDays:localemp.vacDays
        });
      console.log(`Data Cahnged on Emp ${emp}`);
    });
    this.id = this.activatedroute.snapshot.paramMap.get('id');
    if (this.employees.length > 0) {
      let emp = this.employees.find(f => f.id == this.id);
      if(emp)
        this.empForm.setValue({
          name: emp.name,
          vacAccumulate: emp.vacAccumulate,
          employeeType: emp.employeeType,
          workDays: emp.workDays,
          id: emp.id,
          modified: false,
          vacDays: emp.vacDays
      });
    }
   
  }

  

  public doVacDays(e) {
    if (e == 'minus' && this.empForm.value.vacDays > 0)
      this.empForm.value.vacDays -= 1;
    else if (e == 'plus')
      this.empForm.value.vacDays += 1;

    this.empForm.setValue({
      name: this.empForm.value.name,
      vacAccumulate: this.empForm.value.vacAccumulate,
      employeeType: this.empForm.value.employeeType,
      workDays: this.empForm.value.workDays,
      id: this.empForm.value.id,
      modified: true,
      vacDays: this.empForm.value.vacDays
    });
  }

  public doWorkDays(e) {
    if (e == 'minus' && this.empForm.value.workDays>0)
      this.empForm.value.workDays -= 1;
    else if (e == 'plus')
      this.empForm.value.workDays += 1;

    this.empForm.setValue({
      name: this.empForm.value.name,
      vacAccumulate: this.empForm.value.vacAccumulate,
      employeeType: this.empForm.value.employeeType,
      workDays: this.empForm.value.workDays,
      id: this.empForm.value.id,
      modified: true,
      vacDays: this.empForm.value.vacDays
    });
  }

  public doSave() {
    let httpOptions={
      headers: new HttpHeaders({
        'Content-Type':'application/json'
      })
  };
    let empFilterList = this.employees.filter(emp => {
      if (emp.id == this.id) {
        emp.employeeType = this.empForm.value.employeeType;
        emp.name = this.empForm.value.name;
        emp.vacAccumulate = this.empForm.value.vacAccumulate;
        emp.workDays = this.empForm.value.workDays;
        emp.modified = true;
        emp.vacDays = this.empForm.value.vacDays;
      }
      return emp;
    })
    this.localHttp.post<Employees[]>(`${this.localURL}Employee`, empFilterList, httpOptions).subscribe(result => {
      console.log(`data has been return ${result}`);
      if (result) {
        this.employees = result;
        this.myservice.changeDataSource(this.employees);
      }

    });

  }

}
