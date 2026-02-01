using SR.HomeServices.Domain;

namespace SR.HomeServices.Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _repository;
        private readonly DapperContext _context;
        public ProfileService(IProfileRepository repository, DapperContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<ProfileViewModel> GetProfileAsync(int userId)
        {
            return await _repository.GetProfileAsync(userId, _context.CreateConnection());
        }

        public async Task UpsertProfileAsync(ProfileViewModel model, string user)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int addressId = await _repository.UpsertAddressAsync(model, connection, transaction);
                        model.AddressId = addressId;
                        await _repository.UpsertUserProfileAsync(model, user, connection, transaction);
                        await _repository.UpsertUserSkillsAsync(model.UserId, model.Skills, user, connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
