# This file is auto-generated, any changes made to this file will be lost


Feature: Student Editing
	As a student admin
        I want to be able to edit students

Scenario: Should create, delete and check if students exist
			Given a student
			When when adding the student
			Then student should be persisted
			And student should exist
			Then student should be removed

Scenario: Should list many students
			Given many students
			When when adding the students
			Then should be able to retrieve all the added students
			Then students should be removed

Scenario: Should update an existing student
			Given an existing student
			When updating the students firstname, surname and age
			Then student should exist by the new values
			And the previous student data should not exist
			Then student should be removed

Scenario: Should get a student by id
			Given an existing student
			When retrieving a student by id
			Then student should not be null
			Then student should be removed