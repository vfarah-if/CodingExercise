using System;

namespace Exercise.Domain.Hotels
{
    /// <summary>
    ///     Used by the hotel manager to define the types and number of rooms of each type the hotel has. It 
    ///     also can return hotel information given a hotel ID.
    /// </summary>
    /// <remarks>
    ///     Rules: 
    ///         1. The SetRoomType method should create a hotel if there are no hotels with the received ID and update its room type according to the information received.
    ///         2. A change in quantity of rooms should not not affect existing bookings.
    ///         3. They will only affect new bookings, made after the change.
    ///         4. The FindHotelBy should return all the information about number of rooms for the specified ID.
    /// </remarks>
    public class HotelService
    {
        private readonly HotelRepository _hotelRepository;

        public HotelService(HotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public void SetRoomType(Guid hotelId, Guid roomTypeId, int quantity)
        {
            _hotelRepository.AddRoomType(hotelId, roomTypeId, quantity);
        }

        public Hotel FindHotelBy(Guid hotelId)
        {
            throw new NotImplementedException();
        }
    }
}
