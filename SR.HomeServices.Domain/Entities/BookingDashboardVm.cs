namespace SR.HomeServices.Domain.Entities
{
    public class BookingDashboardVm
    {
        public List<BookingItemVm> Pending { get; set; } = new List<BookingItemVm>();
        public List<BookingItemVm> InProgress { get; set; } = new List<BookingItemVm>();
        public List<BookingItemVm> Completed { get; set; } = new List<BookingItemVm>();
    }
}
