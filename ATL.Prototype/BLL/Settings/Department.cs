using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Settings;
using Model.Settings;

namespace BLL.Settings
{
    public class Department
    {
        DAL.Settings.Department _department = new DAL.Settings.Department();
        public List<DepartmentEntity> GetData(string status)
        {
            return _department.GetData(status);
        }
    }
}
