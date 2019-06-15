# This file is auto-generated, any changes made to this file will be lost


Feature: Employee and Booking Policy Administration
	As a Company Administrator
        I want to administer employees and booking policies related to the company and employees

Scenario: Associate Employees with a Company ...
			Given an employee and a company
			When associating the employee with a company
			Then the employee should be linked to the company

Scenario: Employee should be allowed to book any room if there are no company or employee policies
			Given an employee booking policy
			When no company or employee policies exist
			Then the employee booking should be allowed

Scenario: Employee should be allowed to book a room if the employee policy allows this
			Given an employee booking policy, employee and a room type
			When setting an employee policy  for that room type
			Then the employee booking should be allowed

Scenario: Employee should be allowed to book a room if the company policy allows this
			Given an employee booking policy, employee and a room type
			When a setting a company policy exist for that room type
			Then the employee booking should be allowed