using SR.HomeServices.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SR.HomeServices.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DapperContext _context;

        public BookingRepository(DapperContext context)
        {
           _context = context;
        }

        /// <summary>
        /// Gets bookings by status for a specific captain.
        /// </summary>
        /// <param name="captainId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BookingItemVm>> GetBookingsByStatusAsync(int captainId, BookingStatus status)
        {
            var sql = @"
                SELECT  
                    b.BookingId,
                    u.Email AS CustomerName,
                    st.SkillName,
                    b.ServiceAddress,
                    b.Description,
                    b.CreatedDate
                FROM Bookings b
                INNER JOIN Users u ON u.UserId = b.CustomerId
                INNER JOIN SkillTypes st ON st.SkillId = b.SkillId
                LEFT JOIN BookingAssignments a ON a.BookingId = b.BookingId
                WHERE b.BookingStatus = @Status
                  AND b.IsActive = 1
                  AND (a.CaptainId = @CaptainId OR @CaptainId IS NULL)
                ORDER BY b.CreatedDate DESC";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<BookingItemVm>(sql, new
            {
                Status = (int)status,
                CaptainId = captainId
            });
        }
    }
}
