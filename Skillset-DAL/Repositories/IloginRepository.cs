using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_DAL.Repositories
{
    public interface ILoginRepository
    {
        /// <summary>
        /// To get role of an user
        /// </summary>
        /// <param name="employeecode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        string GetRole(string employeecode, string password);
    }
}
