export interface TimeLog {
    userId: number;
    projectId: number;
    date: Date;
    hoursWorked: number;
    user: {
      name: string;
      email: string;
    };
    project: {
      name: string;
    };
  }