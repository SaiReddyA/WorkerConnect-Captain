
namespace SR.HomeServices.Domain
{
    public class ProfileViewModel
    {
        public long UserId { get; set; }

        // Profile
        public long? ProfileId { get; set; }
        public string FullName { get; set; }

        // Address
        public long? AddressId { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Pincode { get; set; }

        // Skills (Checkboxes)
        public List<SkillCheckboxViewModel> Skills { get; set; } = new();
    }
    public class SkillCheckboxViewModel
    {
        public long SkillId { get; set; }
        public string SkillName { get; set; }
        public bool IsSelected { get; set; }
        public bool IsVerified { get; set; }
    }

}
