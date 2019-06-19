using System;
using System.Linq;
using CoreBDD;
using Exercise.Domain.Bookings;
using Exercise.Domain.Companies;
using Exercise.Domain.Hotels;
using FluentAssertions;

namespace Exercise.Domain.Tests.AcceptanceTests
{
    public class EmployeeShould: EmployeeHotelBookingFeature
    {
        private readonly BookingService _bookingService;
        private BookingPolicyRepository _employeeBookingPolicyRepository;
        private BookingPolicyRepository _companyBookingPolicyRepository;
        private BookingPolicyService _bookPolicyService;
        private CompanyService _companyService;
        private CompanyRepository _companyRepository;
        private HotelService _hotelService;
        private HotelRepository _hotelRepository;
        private Company _company;
        private Hotel _hotel;
        private readonly BookingRepository _bookingRepository;

        public EmployeeShould()
        {
            SetupCompanyAndDependencies();
            SetupBookingPolicyServiceAndDependencies();
            SetupHotelServiceAndDependencies();
            _bookingRepository = new BookingRepository();
            _bookingService = new BookingService(_bookPolicyService, _hotelService, _bookingRepository);
        }    

        [Scenario("Allows employees to book rooms at hotels")]
        public void BeAbleToBookAHotel()
        {
            BookingStatus actualBookingStatus = null;
            DateTime checkIn = DateTime.Today.AddDays(1);
            DateTime checkOut = DateTime.Today.AddDays(2);

            Given("a company with an employee", () =>
            {
                _company.Should().NotBeNull();
                _company.Employees.Should().NotBeNullOrEmpty();
            });
            And("a Hotel with a room type", () =>
            {
                _hotel.Should().NotBeNull();
                _hotel.TotalRoomCount.Should().BeGreaterOrEqualTo(1);
            });
            When("an employee books a hotel starting tomorrow and checkout the day after", () =>
            {
                actualBookingStatus = actualBookingStatus =
                        _bookingService.Book(_company.Employees.First().Id, _hotel.Id, _hotel.RoomTypes.First(), checkIn, checkOut);
            });
            Then("the employee should get a a booking confirmation with all the relevant information", () =>
            {
                actualBookingStatus.IsBooked.Should().BeTrue();
                actualBookingStatus.HotelId.Should().Be(_hotel.Id);
                actualBookingStatus.GuestId.Should().Be(_company.Employees.First().Id);
                actualBookingStatus.StartDate.Should().Be(checkIn);
                actualBookingStatus.EndDate.Should().Be(checkOut);
                _bookingRepository.List().Should().Contain(actualBookingStatus);
            });
        }

        private void SetupCompanyAndDependencies()
        {
            _companyRepository = new CompanyRepository();
            _companyService = new CompanyService(_companyRepository);
            var companyId = Guid.NewGuid();
            var employeeId = Guid.NewGuid();
            _companyService.AddEmployee(companyId, employeeId);
            _company = _companyService.FindCompany(companyId);
        }

        private void SetupHotelServiceAndDependencies()
        {
            _hotelRepository = new HotelRepository();
            _hotel = _hotelRepository.Add(new Hotel());
            _hotelService = new HotelService(_hotelRepository);
            _hotelService.SetRoomType(_hotel.Id, Guid.NewGuid(), 1);
            _hotelService.SetRoomType(_hotel.Id, Guid.NewGuid(), 2);
            _hotelService.SetRoomType(_hotel.Id, Guid.NewGuid(), 3);            
        }

        private void SetupBookingPolicyServiceAndDependencies()
        {
            _employeeBookingPolicyRepository = new BookingPolicyRepository();
            _companyBookingPolicyRepository = new BookingPolicyRepository();
            _bookPolicyService = new BookingPolicyService(_employeeBookingPolicyRepository, _companyBookingPolicyRepository);
        }

    }
}
