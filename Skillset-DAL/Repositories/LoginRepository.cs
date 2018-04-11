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
        /// <summary>
        /// To get role of a user
        /// </summary>
        /// <param name="employeecode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string GetRole(string employeecode, string password)
        {
            try
            {
                using (SkillsetDbContext db = new SkillsetDbContext())
                {
                    double pass = Convert.ToDouble(password);
                    //get status of employee
                    var status = (from employee in db.Employees where (employee.EmployeeCode == employeecode && employee.MobileNumber == pass) select (employee.Status)).FirstOrDefault();
                    if (status == true)
                    {
                        //get role of employee
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
