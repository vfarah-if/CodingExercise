# This file is auto-generated, any changes made to this file will be lost


Feature: Student Editing
	As a student admin
        I want to be able to edit students

Scenario: Should create, delete and check if students exist
			Given a student
			When when creating the student
			Then student should be persisted
			And student should exist
			And student should then be removed