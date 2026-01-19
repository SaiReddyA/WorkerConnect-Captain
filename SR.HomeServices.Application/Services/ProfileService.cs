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
            using(var connection = _context.CreateConnection())
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Closed) {
                    //connection = _context.CreateConnection();
                }
                var trasaction = connection.BeginTransaction();
                try
                {
                    int addressId = await _repository.UpsertAddressAsync(model, connection, trasaction);
                    model.AddressId = addressId;
                    await _repository.UpsertUserProfileAsync(model, user, connection, trasaction);
                    await _repository.UpsertUserSkillsAsync(model.UserId, model.Skills, user, connection, trasaction);
                }
                catch (Exception ex)
                {
                    trasaction.Rollback();
                }
            }
        }
    }
}
