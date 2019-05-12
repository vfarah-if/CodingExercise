# This file is auto-generated, any changes made to this file will be lost


Feature: Student Editing
	As a student admin
        I want to be able to edit students

Scenario: Should create, delete and check if students exist
			Given several students
			When when adding the students
			Then student should be persisted
			And student should exist
			And student should then be removed

Scenario: Should list many students
			Given many students
			When when adding the students
			Then should be able to retrieve all the added students
			And students should then be removed