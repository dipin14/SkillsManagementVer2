using Skillset_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_BLL.Services
{
    public class LoginService:ILoginService
    {
        private readonly ILoginRepository logRepository;

        /// <summary>
        /// Dependency Injection for ILoginRepository
        /// </summary>
        /// <param name="logRepo"></param>
        public LoginService(ILoginRepository logRepo)
        {
            logRepository = logRepo;
        }
        /// <summary>
        /// To get role of a user
        /// </summary>
        /// <param name="employeecode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string GetRole(string employeecode, string password)
        {
            var role = logRepository.GetRole(employeecode, password);
            return role;
        }
    }
}
