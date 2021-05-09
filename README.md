# Music-catalog

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)

## General info
Web application for presenting the music catalog. The program is a recruitment project for an internship.
	
## Technologies
Project is created with:
* ASP .NET CoreWeb Api version: 3.1
* Angular CLI version: 11.2.9
* Visual Studio 2019
	
## Setup
To run the MusicCatalogAPI project you first need to create a database on mssqllocaldb. 
In Visual Studio, go to Tools -> NuGet Package Manager -> Package Manager Console. 
In the console: 
```
$ Update-Database 
```
Now you can start the application by clicking the 'IIS Express' button or by pressing the F5 keyboard shortcut.
To run Angular project:
```
$ cd ../MusicCatalogAngular
$ ng serve install
```
Navigate to `http://localhost:4200/` to use the client app.

Navigate to `http://localhost:49781/swagger/index.html` to use swagger.
