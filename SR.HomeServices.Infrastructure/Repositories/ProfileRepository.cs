using SR.HomeServices.Domain;

namespace SR.HomeServices.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        public ProfileRepository()
        {
        }

        public async Task<ProfileViewModel> GetProfileAsync(int userId, IDbConnection connection)
        {
            const string profileSql =
                "SELECT ProfileId, FullName FROM UserProfiles WHERE UserId = @UserId";

            const string addressSql =
                "SELECT AddressId, AddressLine, City, Area, Pincode FROM Addresses WHERE UserId = @UserId";

            const string skillsSql =
            @"SELECT s.SkillId, s.SkillName,
                CASE WHEN us.UserSkillId IS NULL THEN 0 ELSE 1 END AS IsSelected,
                ISNULL(us.IsVerified, 0) AS IsVerified
              FROM SkillTypes s
              LEFT JOIN UserSkills us 
                ON s.SkillId = us.SkillId AND us.UserId = @UserId
              WHERE s.IsActive = 1";

            var profile = await connection.QueryFirstOrDefaultAsync(profileSql, new { UserId = userId });
            var address = await connection.QueryFirstOrDefaultAsync(addressSql, new { UserId = userId });
            var skills = (await connection.QueryAsync<SkillCheckboxViewModel>(skillsSql, new { UserId = userId })).ToList();

            return new ProfileViewModel
            {
                UserId = userId,
                ProfileId = profile?.ProfileId,
                FullName = profile?.FullName,
                AddressId = address?.AddressId,
                AddressLine = address?.AddressLine,
                City = address?.City,
                Area = address?.Area,
                Pincode = address?.Pincode,
                Skills = skills
            };
        }

        public async Task UpsertUserProfileAsync(ProfileViewModel model, string user, IDbConnection connection, IDbTransaction transaction)
        {
            const string sql = @"
            IF EXISTS (SELECT 1 FROM UserProfiles WHERE UserId = @UserId)
            BEGIN
                UPDATE UserProfiles
                SET FullName = @FullName,
                    UpdatedBy = @User,
                    UpdatedDate = GETUTCDATE()
                WHERE UserId = @UserId
            END
            ELSE
            BEGIN
                INSERT INTO UserProfiles
                (UserId, AddressId, FullName, IsActive, CreatedBy, CreatedDate)
                VALUES (@UserId, @AddressId, @FullName, 1, @User, GETUTCDATE())
            END";

            await connection.ExecuteAsync(sql, new
            {
                model.UserId,
                model.FullName,
                @AddressId = model.AddressId,
                User = user
            }, transaction);
        }

        public async Task<int> UpsertAddressAsync(ProfileViewModel model, IDbConnection connection, IDbTransaction transaction)
        {
            const string sql = @"
            IF EXISTS (SELECT 1 FROM Addresses WHERE UserId = @UserId)
            BEGIN
                UPDATE Addresses
                SET AddressLine = @AddressLine,
                    City = @City,
                    Area = @Area,
                    Pincode = @Pincode
                WHERE UserId = @UserId;

                SELECT AddressId FROM Addresses WHERE UserId = @UserId;
            END
            ELSE
            BEGIN
                INSERT INTO Addresses
                (UserId, AddressLine, City, Area, Pincode, IsActive, CreatedDate)
                VALUES (@UserId, @AddressLine, @City, @Area, @Pincode, 1, GETUTCDATE());

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            END";

            return await connection.ExecuteScalarAsync<int>(sql, model, transaction);
        }


        public async Task UpsertUserSkillsAsync(long userId, List<SkillCheckboxViewModel> skills, string user, IDbConnection connection, IDbTransaction transaction)
        {
            const string insertSql = @"
            INSERT INTO UserSkills
            (UserId, SkillId, IsVerified, VerificationStatus, IsActive, CreatedBy, CreatedDate)
            VALUES (@UserId, @SkillId, 0, 'Pending', 1, @User, GETUTCDATE())";

            const string deleteSql = "DELETE FROM UserSkills WHERE UserId = @UserId AND SkillId = @SkillId";

            try
            {
                foreach (var skill in skills)
                {
                    if (skill.IsSelected)
                    {
                        await connection.ExecuteAsync(insertSql, new
                        {
                            UserId = userId,
                            skill.SkillId,
                            User = user
                        }, transaction);
                    }
                    else
                    {
                        await connection.ExecuteAsync(deleteSql, new
                        {
                            UserId = userId,
                            skill.SkillId
                        }, transaction);
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
