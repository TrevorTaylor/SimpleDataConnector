Simple Data Connector Revit 2025 Add-in

Template: Revit Addin (Nice3point)

Template options:

Project name: 	SimpleDataConnector
Location: 	<folder>
Place solution and project in the same directory: Yes
Add-In type: 	Command
User Interface: Modal
IoC: 		Disabled
Serilog support: No

Set your configuration to "Debug_25"

Data Context Dependencies:

Right-click Dependencies and use Manage Nuget Packages, Browse tab, to add the following packages:

Microsoft.EntityFrameworkCore by Microsoft
Microsoft.EntityFrameworkCore.SqlServer by Microsoft

Create a class for your DbContext. Your data context consists of one class to manage the connection and one class for EACH database table in your database(s). 

Note: Mapping DB data types to .NET data types can be tricky. For example in this exercise, I just picked an unused DB stored on ZGF\PDX-SQL-1-DTECH at random. "Float" (bad choice for these fields) must be mapped "Double."  This DB schema has some structural problems. "Survey ID" and "Appears in Sheet List" should both be an integer. Also, including spaces in field names, while allowed, makes property to field mapping more troublesome. Another problem is that the "Survey ID" should be marked as "Key" in the DB. This would disallow duplicate values and would cause the field to auto-increment when new records are added.
