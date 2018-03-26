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
        public LoginService(ILoginRepository logRepo)
        {
            logRepository = logRepo;
        }

        public string GetRole(string employeecode, string password)
        {
            var role = logRepository.GetRole(employeecode, password);
            return role;
        }
    }
}
