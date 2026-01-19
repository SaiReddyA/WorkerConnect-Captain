using System;
using SR.HomeServices.Domain;
namespace SR.HomeServices.Application.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileViewModel> GetProfileAsync(int userId);
        Task UpsertProfileAsync(ProfileViewModel model, string loggedInUser);
    }
}
