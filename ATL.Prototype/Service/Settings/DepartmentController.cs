using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using Model.Settings;
using BLL.Settings;

namespace Service.Settings
{
    public class DepartmentController : ApiController
    {
        Department _department = new Department();
        public List<DepartmentEntity> GetData()
        {
            return _department.GetData("1");
        }
    }
}
