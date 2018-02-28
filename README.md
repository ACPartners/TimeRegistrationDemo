
# TimeRegistrationDemo

## Installation Requirements

See [Installation Guide](Installation%20Guide.md)

## Business case

### Web

#### Functionalities

##### Login
- Employee
- Employer
- System administrator

##### Employee functionalities
- Register holiday
  - From date
  - Until date
  - Type of holiday
  - Remarks
- Revoke holiday request
- List of holidays (by year)
  - Total number of all holidays
  - Total number of approved holidays
  - Total number of holidays to be approved
  - Total number of holidays (grouped by Type of holiday)
  - Overview of approved holidays
    - From date
    - Until date
    - Type of holiday
    - Remarks
  - Overview of holidays to be approved (same data as above)

##### Employer functionalities
- Accept/Decline holiday request of employees
  - Approved/Declined
  - Decline reason
- Reports
  - List holidays by person (same as "List of holidays" of Employee)
  - List holidays (overview of all employees)
##### System administrator functionalities
- Change type of holidays (Paid holiday, Normal holiday, Sick-leave, Maternity leave)

#### Technical

##### Backend services

###### Register holiday 
- Validations
  - From date is before Until date
  - Valid Type of holiday
  - Holiday is not yet in database

###### Revoke holiday request
- Validations
  - Not yet approved

###### List of holidays (by year)
- Employee
  - Data
    - Total number of all holidays
    - Total number of approved holidays
    - Total number of holidays to be approved
    - Total number of holidays (grouped by Type of holiday) (dynamic array)
    - Overview of approved holidays
      - From date
      - Until date
      - Type of holiday
      - Remarks (first 100 characters)
  - Filter
    - Year: if none provided (default): current year
  - Ordered by From date
- Employer: Same as for employee but extra filter: Employee

###### List of Holidays (overview of all employees)
Same as for employee but extra grouping level: Employee

###### Accept holiday request of employees

###### Decline holiday request of employees
- Validations
  - Decline reason

###### Change type of holidays
- Validations
  - Check uniqueness of Type of holiday
- Initial values
  - Paid holiday
  - Normal holiday
  - Sick-leave
  - Maternity leave

##### Authentication
To investigate:
- How to make the distinction between employer and employee and admin? Possible user table in database with predefined users and assigned role
- Secure api calls
- Change api behaviour based on authentication

Information:
https://fullstackmark.com/post/13/jwt-authentication-with-aspnet-core-2-web-api-angular-5-net-core-identity-and-facebook-login

##### Screens
not needed for backend for the moment :-)

##### TO DISCUSS
Do we need a more complex example (with a one-to-many relationship between "tables/entities")

Future functionalities or do we provide timesheets:
- Register timesheet (employee)
- List timesheets (employee)
- Accept/Decline timesheet of employees (employer)
- Change timesheet categories (application development, support,...)
- ...

## Design goals 2018

### Technologies
 - C#
 - Use .NET Core
 - Use ASPNETCore
 - Use Docker as hoster
 - EntityFramework (Code First + migrations)
 - Sql server 2016 (localdb and real server)
 - Visual Studio 2017 Community Edition
 - Windows 10
 - Git
 - Validation libraries (FluentValidation)
 - Mapping libraries (AutoMapper)

### Design methodologies
 - [CQRS](CQRS%20Basics.md)
 - DDD
 - REST API
 - IOC containers ( Windsor, Unity, Autofac,...)
 - Source control choices
 - Kanban
 - Scrum
 - Design Patterns
 - Logging & Exception handling
 - Minimum Viable Product

## Future Ideas
 - Use Azure as hoster
 - In depth tutorials for some subjects:
	 - Experiences with code migration (Filip)
	 - Big data ( Geoffrey )
 - Writing a front-end for this in:
	 - web with Angular 5 / React / Vue 
	 - mobile with Xamarin
	 - desktop xaml
	 - VR with Unity3D