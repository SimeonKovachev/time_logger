# Time Logger Application

## Overview
Time Logger is a web-based application designed for tracking and managing time logs for various projects and users. It's built using ASP.NET Core and follows the MVC (Model-View-Controller) architecture with Razor pages for the frontend. The application integrates Syncfusion components to enhance its UI and functionality.


## Main Goal
The primary goal of this application is to provide an efficient and user-friendly platform for tracking time spent on different projects by various users. It facilitates the creation, management, and visualization of time logs.


## Key Features

* User Management: Create and manage user profiles.
- Project Management: Add and manage projects.
* Time Logging: Log time spent on different projects.
- Data Visualization: View and analyze time logs.
* Database Initialization: Seed the database with initial data for testing and demonstration.
* Syncfusion Components: Utilize Syncfusion's UI components for a more interactive and visually appealing user interface.


## Controllers

- ProjectsController: Manages operations related to projects.
- TimeLogsController: Handles time log creation and retrieval.
- UsersController: Responsible for user-related operations.


## Models

- User: Represents user profiles.
- Project: Defines projects.
- TimeLog: Captures time logs with details like date and hours worked.


## Views (Razor Pages)

- MVC Views using Razor syntax for dynamic HTML rendering.
- Pages for displaying user profiles, project details, and time logs.
- Interactive UI elements for a seamless user experience.
- Integration of Syncfusion components for enhanced UI features.


## Database

- Uses Entity Framework Core with a SQL Server database.
- TimeLoggerDbContext: Manages entities like Users, Projects, and TimeLogs.
- DbInitializer: Seeds the database with sample data.


## Running the Application on Windows
 ### Prerequisites
- .NET Core SDK
- SQL Server (LocalDB or other)


### Setup

1. Navigate to the time_logger directory.
2. Update the connection string in appsettings.json if necessary.
3. Run dotnet restore to install dependencies.
4. Run dotnet run to start the application.
5. Access the application at http://localhost:[port].
6. Keep in mind that time_logger_ui is the uncompleted angular attempt.


## Conclusion

Time Logger offers a robust backend and a dynamic frontend using MVC and Razor pages, ideal for tracking and managing time logs across various projects. The integration of Syncfusion components further enhances the application's usability and visual appeal.
