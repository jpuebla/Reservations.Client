using Reservations.Backend.Models;

namespace Reservations.Client.Service
{
    public interface IReservationService
    {
        Task<List<Reservation>> GetReservations(DateOnly date, int? shiftId, int? tableId);
        Task<List<Shift>> GetShifts();
        Task<List<RestaurantTable>> GetTables();
    }
}