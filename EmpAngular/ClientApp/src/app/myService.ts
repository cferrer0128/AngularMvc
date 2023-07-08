import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Employees } from './IEmployee';

@Injectable({
  providedIn: 'root'
})
export class Myservice {
  private dataSource = new BehaviorSubject<Employees[]>([]);
  currentSource = this.dataSource.asObservable();
  constructor() { }

  changeDataSource(data: Employees[]) {
    this.dataSource.next(data);
  }
}
