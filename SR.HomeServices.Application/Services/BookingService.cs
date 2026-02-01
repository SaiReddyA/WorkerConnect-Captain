using SR.HomeServices.Domain.Enums;

namespace SR.HomeServices.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;

        public BookingService(IBookingRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets bookings by status for a specific captain.
        /// </summary>
        /// <param name="captainId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BookingItemVm>> GetBookingsByStatusAsync(int captainId, BookingStatus status)
        {
            return await _repository.GetBookingsByStatusAsync(captainId, status);
        }

        /// <summary>
        /// Gets the booking dashboard for a specific captain.
        /// </summary>
        /// <param name="captainId"></param>
        /// <returns></returns>
        public async Task<BookingDashboardVm> GetDashboardAsync(int captainId)
        {
            return new BookingDashboardVm
            {
                Pending = (await _repository.GetBookingsByStatusAsync(captainId, BookingStatus.Pending)).ToList(),
                InProgress = (await _repository.GetBookingsByStatusAsync(captainId, BookingStatus.InProgress)).ToList(),
                Completed = (await _repository.GetBookingsByStatusAsync(captainId, BookingStatus.Completed)).ToList()
            };
        }
    }
}
