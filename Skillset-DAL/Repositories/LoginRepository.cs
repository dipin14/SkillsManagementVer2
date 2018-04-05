using Skillset_DAL.ContextClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillset_DAL.Repositories
{
    public class LoginRepository:ILoginRepository
    {
        public string GetRole(string employeecode, string password)
        {
            try
            {
                using (SkillsetDbContext db = new SkillsetDbContext())
                {
                    double pass = Convert.ToDouble(password);
                    var status = (from employee in db.Employees where (employee.EmployeeCode == employeecode && employee.MobileNumber == pass) select (employee.Status)).FirstOrDefault();
                    if (status == true)
                    {
                        var role = (from employee in db.Employees join roles in db.Roles on employee.RoleId equals roles.Id where (employee.EmployeeCode == employeecode) select roles.Name).FirstOrDefault();
                        return role;
                    }
                    else
                    {
                        var role = "Not Valid User";
                        return role;
                    }
                }
            }
            catch (Exception)
            {
                return "Not Valid User";
            }

        }
    }
}
