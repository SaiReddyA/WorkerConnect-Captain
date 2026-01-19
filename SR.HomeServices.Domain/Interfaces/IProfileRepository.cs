
using System.Data;

namespace SR.HomeServices.Domain.Interfaces
{
    public interface IProfileRepository
    {
        Task<ProfileViewModel> GetProfileAsync(int userId, IDbConnection connection);
        Task UpsertUserProfileAsync(ProfileViewModel model, string user, IDbConnection connection, IDbTransaction transaction);
        Task<int> UpsertAddressAsync(ProfileViewModel model, IDbConnection connection, IDbTransaction transaction);
        Task UpsertUserSkillsAsync(long userId, List<SkillCheckboxViewModel> skills, string user, IDbConnection connection, IDbTransaction transaction);
    }
}
