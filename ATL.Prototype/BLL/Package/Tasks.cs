using Model.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Package
{
    public class Tasks
    {
        DAL.Package.Tasks _Tasks = new DAL.Package.Tasks();
        public List<PACKAGE_BASE_INFO_Entity> GetTasks(string PMES_USER_ID)
        {
            return _Tasks.GetTasks(PMES_USER_ID);
        }
    }
}
