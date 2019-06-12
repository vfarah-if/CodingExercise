# This file is auto-generated, any changes made to this file will be lost


Feature: Employee and Booking Policy Administration
	As a Company Administrator
        I want to administer employees and booking policies related to the company and employees

Scenario: Associate Employees with a Company ...
			Given a valid employee and a company
			When associating the employee with a company
			Then the employee should now be associated with this company