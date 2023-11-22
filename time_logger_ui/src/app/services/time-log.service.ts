import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TimeLog } from '../models/time-log';

@Injectable({
  providedIn: 'root'
})
export class TimeLogService {
  private apiUrl = 'http://localhost:5000/api/timelogs'; // Replace with your API URL

  constructor(private http: HttpClient) {}

  getTimeLogs(page: number, pageSize: number, sortField: string, sortOrder: string, dateFrom?: Date, dateTo?: Date): Observable<TimeLog[]> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString())
      .set('sortField', sortField)
      .set('sortOrder', sortOrder);

      if (dateFrom) {
        params = params.set('dateFrom', dateFrom.toISOString().split('T')[0]);
      }
      if (dateTo) {
        params = params.set('dateTo', dateTo.toISOString().split('T')[0]);
      }
  
      return this.http.get<TimeLog[]>(this.apiUrl, { params });
  }

  // Additional methods for other operations can be added here
}

