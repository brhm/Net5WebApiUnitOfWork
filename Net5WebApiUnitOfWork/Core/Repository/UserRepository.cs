using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Net5WebApiUnitOfWork.Data;
using Net5WebApiUnitOfWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5WebApiUnitOfWork.Core.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<User>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(UserRepository));
                return new List<User>();
            }
        }
        public override async Task<bool> Update(User entity)
        {
            try
            {
                var existUser = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
                if (existUser == null)
                    return await Add(entity);

                existUser.FirstName = entity.FirstName;
                existUser.LastName = entity.LastName;
                existUser.Email = entity.Email;
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(UserRepository));
                return false;
            }

        }
        public override async Task<bool> Delete(Guid Id)
        {
            try
            {
                var existUser = await dbSet.Where(x => x.Id == Id).FirstOrDefaultAsync();
                if (existUser == null)
                    return false;
                dbSet.Remove(existUser);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(UserRepository));
                return false;
            }
        }
    }
}
