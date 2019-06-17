# This file is auto-generated, any changes made to this file will be lost


Feature: Employee Hotel Bookings
	As an Employee
        I want to book a hotel room

Scenario: Allows employees to book rooms at hotels
			Given a company with an employee
			And a Hotel with a room type
			When an employee books a hotel starting tomorrow and checkout the day after
			Then the employee should get a a booking confirmation with all the relevant information