# Bussiness Case Design Document (initial draft)

## Introduction

After exploring a couple of ideas, we decided to make a own version of timesheet management app because this is the only software we all share knowledge about!  
We don't want to drive the concept too far but we still want to have some things be challenging enough to warant some thought about how to design it.

## Bussiness Domain

### Entities

#### User

 Id (int)  
 FirstName(string)  
 LastName(string)  
 UserRoles (1 to many)  
 
#### UserRole

 Id (string)  
 TypeDesc (string)  

#### HolidayRequest

Id (long)  
From (date)  
To   (date)  
Remarks ( string)  
HolidayType  
IsApproved  (bool)  
DisApprovedReason (string)
CreationDate (datetime) 

#### HolidayType

Id (string)  
Description (string)  


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

###### Create User
- Validations
  - FirstName: length: 2-50
  - LastName: length: max 50
  - Combination of FirstName and LastName is unique.

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
