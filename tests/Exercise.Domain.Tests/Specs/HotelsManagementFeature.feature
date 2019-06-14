# This file is auto-generated, any changes made to this file will be lost


Feature: Hotel Rooms And Quantities
	As a Hotel Manager
        I want to set all the different types of rooms and respective quantities for my hotel

Scenario: Define the number of room types a hotel supports
			Given a hotel id, room type and quantity
			When setting the room type information
			Then expect the hotel to include the all provided information