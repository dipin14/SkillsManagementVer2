using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_BLL.Services
{
    public interface ILoginService
    {
        string GetRole(string employeecode, string password);
    }
}
