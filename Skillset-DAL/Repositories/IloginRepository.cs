using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_DAL.Repositories
{
    public interface IloginRepository
    {
        string GetRole(string employeecode, string password);
    }
}
