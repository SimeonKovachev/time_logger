import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserTimeLogsGridComponent } from './user-time-logs-grid.component';

describe('UserTimeLogsGridComponent', () => {
  let component: UserTimeLogsGridComponent;
  let fixture: ComponentFixture<UserTimeLogsGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserTimeLogsGridComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserTimeLogsGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
