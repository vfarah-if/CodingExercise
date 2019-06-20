# This file is auto-generated, any changes made to this file will be lost


Feature: Coding Exercise
	As a user
        I want to convert time in the format of hh:mm:ss to the expected berlin clock format

Scenario: Format hh:mm:ss format to berlin clock format
			Given 12:56:01 and a berlin clock converter
			When converting the time
			Then an expectation should be satisfied outputting '" + expected + "'