using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PMS.Data.Repositories;
using PMS.Domain;
using PMS.Services.Interface;
using PMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services.Implementation
{

    public class AccountService: IAccount
    {
        #region Declaration
        private readonly ILogger _logger;
        private readonly IRepository<User> _usersRepo;
        #endregion

        #region ctor
        public AccountService(ILogger<AccountService> logger,
                                 IRepository<User> usersRepo)
        {
            _logger = logger;
            _usersRepo = usersRepo;
        }
        #endregion
        public User CheckUserExistence(LoginUserViewModel model)
        {
            try
            {
                var checkUser = _usersRepo.Query().Include(x => x.Role).Where(x => x.UserName == model.userName && x.Password == model.password).FirstOrDefault();
                if (checkUser != null)
                {
                    return checkUser;
                }
                else
                {
                    return new User();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
