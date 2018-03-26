using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_DAL.Repositories
{
    public interface ILoginRepository
    {
        string GetRole(string employeecode, string password);
    }
}
