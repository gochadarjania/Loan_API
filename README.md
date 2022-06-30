# Loan API 

# Contents
* [Programs](#Programs)
* [About](#About)
* [Instalation](#Instalation)
* [Features](#Features)
* [Testing](#testing)

## Programs
* Visual Studio 2022
* SQL Server Management Studio
* Postman

## About

* NET 5
* MSSQL Server
* NLog
* MediatR
* FluentValidation
* JWT Authentication and Authorization
* BCrypt
* AutoMapper
* Entity Framework Core
* XUnit

## Instalation

* Clone github repository, or download and unzip it.
* Create Migration  

 <pre>update-database</pre>
    
* Create Log Table In Data Base (Script)
    
<pre>
USE [LoanAPI]
GO

/****** Object:  Table [dbo].[Logs]    Script Date: 28.06.2022 02:34:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Level] [nvarchar](10) NULL,
	[Message] [nvarchar](max) NULL,
	[StackTrace] [nvarchar](max) NULL,
	[Exception] [nvarchar](max) NULL,
	[Logger] [nvarchar](255) NULL,
	[Url] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


</pre>

    
## Features

* User registration;
<pre>{
  "firstName": "string",
  "lastName": "string",
  "userName": "string",
  "password": "string",
  "age": 0,
  "salary": 0
}</pre>
* Password hashing;
* Role-based authorization;
* User Login via access token creation;
  * Role - Accountant
<pre>
{
  "userName": "admin@gmail.com",
  "password": "admin1234"
}</pre>
  * Role - User
<pre>
{
  "userName": "user4@gmail.com",
  "password": "admin1234"
}</pre>


* Role - Accountant
  * Delete Loan Of User
  * Update Loan By User Id
  * Update User Status
  * Get All Loans of User
  
* Role - User
  * Create Loan (if IsBlocked = false)
  * Delete Loan (if IsBlocked = false, Loan Status = "in progress")
  * Update Loan (if IsBlocked = false, Loan Status = "in progress")
  * Update User Info (if IsBlocked = false)
  * Get User Info
  * Get Loan 
  * Get all Loans
  
## Testing
The first step is to log in (username and password). Get Generated Token. Token format is "Bearer Access Token".
[Postman Collection](https://github.com/gochadarjania/Loan_API/tree/master/Postman%20Collection)
