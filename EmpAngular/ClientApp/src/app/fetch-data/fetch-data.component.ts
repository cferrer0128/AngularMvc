import { Component, Inject , OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Employees } from '../IEmployee';
import { Myservice } from '../myservice';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  public employees: Employees[];
  localURL: string;
  localHttp: HttpClient;

  public addEmployee() {
    
  }
  public editEmployee(emp) {

    console.log(`${emp}`);
    this.router.navigate([`employee/${emp.id}`]);

  }

  constructor(private myservice: Myservice , private router: Router,http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.localURL = baseUrl;
    this.localHttp = http;
  }
    ngOnInit(): void {
      this.myservice.currentSource.subscribe(emp => {
        this.employees = emp;
        console.log(`Data Cahnged on fetch ${emp}`);

      });

      if (this.employees.length<=0)
        this.localHttp.get<Employees[]>(this.localURL + 'Employee').subscribe(result => {
          this.employees = result;
          this.myservice.changeDataSource(this.employees);
        }, error => console.error(error));
    }
}

