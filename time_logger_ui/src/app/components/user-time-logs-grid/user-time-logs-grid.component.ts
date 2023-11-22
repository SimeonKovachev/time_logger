import { Component, OnInit } from '@angular/core';
import { TimeLogService } from '../../services/time-log.service';
import { TimeLog } from '../../models/time-log';

@Component({
  selector: 'app-user-time-logs-grid',
  templateUrl: './user-time-logs-grid.component.html',
  styleUrls: ['./user-time-logs-grid.component.css']
})

export class UserTimeLogsGridComponent implements OnInit {
changePage(arg0: number) {
throw new Error('Method not implemented.');
}
  timeLogs: TimeLog[] = [];
  currentPage = 1;
  pageSize = 10;
  sortField = 'date'; // Default sort field
  sortOrder = 'asc'; // Default sort order
  dateFrom?: Date;
  dateTo?: Date;
  
  constructor(private timeLogService: TimeLogService) {}

  ngOnInit(): void {
    this.fetchTimeLogs();
  }
  

  fetchTimeLogs(): void {
    this.timeLogService.getTimeLogs(this.currentPage, this.pageSize, this.sortField, this.sortOrder, this.dateFrom, this.dateTo)
      .subscribe(data => {
        this.timeLogs = data;
        // Handle pagination data if your API provides it
      });
  }
  
  onSortChange(field: string): void {
    this.sortField = field;
    this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
    this.fetchTimeLogs();
  }

  onDateFilterChange(): void {
    this.fetchTimeLogs();
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.fetchTimeLogs();
  }
}
