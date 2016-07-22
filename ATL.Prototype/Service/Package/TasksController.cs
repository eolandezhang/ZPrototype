using Model.Package;
using BLL.Package;
using System.Collections.Generic;
using System.Web.Http;

namespace Service.Package
{
    public class TasksController : ApiController
    {
        BLL.Package.Tasks _Tasks = new BLL.Package.Tasks();
        public List<PACKAGE_BASE_INFO_Entity> GetTasks(string PMES_USER_ID)
        {
            return _Tasks.GetTasks(PMES_USER_ID);
        }
    }
}
