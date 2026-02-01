namespace SR.HomeServices.Domain.Entities
{
    public class BookingItemVm
    {
        public int BookingId { get; set; }
        public string CustomerName { get; set; }
        public string SkillName { get; set; }
        public string ServiceAddress { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
