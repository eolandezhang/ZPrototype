using Model.Package;
using BLL.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;


namespace Service.Package
{
    public class PreviewController : ApiController
    {
        BLL.Package.Preview _Preview = new BLL.Package.Preview();
        BLL.Package.PACKAGE_WF_STEP _PACKAGE_WF_STEP = new BLL.Package.PACKAGE_WF_STEP();
        [HttpGet]
        public bool HasBeginWf(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            return _Preview.HasBeginWf(PACKAGE_NO, VERSION_NO, FACTORY_ID);
        }

        [HttpGet]
        public int Init_PACKAGE_WF_STEP(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            return _Preview.Init_PACKAGE_WF_STEP(PACKAGE_NO, VERSION_NO, FACTORY_ID);
        }

        public Model.Package.Preview PostAuditInfo(Model.Package.PACKAGE_BASE_INFO_Entity entity)
        {
            return _Preview.PostAuditInfo(entity.PACKAGE_NO, entity.VERSION_NO, entity.FACTORY_ID);
        }
        [HttpGet]
        public int Agree(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string PMES_USER_ID, string AUDITOR_COMMENT)
        {
            return _Preview.Agree(PACKAGE_NO, VERSION_NO, FACTORY_ID, PMES_USER_ID, AUDITOR_COMMENT);
        }
        [HttpGet]
        public int Disagree(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string AUDITOR_COMMENT)
        {
            return _Preview.Disagree(PACKAGE_NO, VERSION_NO, FACTORY_ID, AUDITOR_COMMENT);
        }
        public PACKAGE_WF_STEP_Entity GetCurrentPkgWfStep(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            return _Preview.GetCurrentPkgWfStep(PACKAGE_NO, VERSION_NO, FACTORY_ID);
        }
        [HttpGet]
        public string AuditHistory(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            return _Preview.AuditHistory(PACKAGE_NO, VERSION_NO, FACTORY_ID);
        }
        [HttpGet]
        public List<Model.Settings.PMES_USER_GROUP_INFO_Entity> GetWfSetAuditorByPkgWfAuditorId(decimal AUDITOR_ID, string FACTORY_ID)
        {
            return _Preview.GetWfSetAuditorByPkgWfAuditorId(AUDITOR_ID, FACTORY_ID);
        }
        [HttpGet]
        public int ChangeAuditor(decimal AUDITOR_ID, string PMES_USER_ID)
        {
            return _Preview.ChangeAuditor(AUDITOR_ID, PMES_USER_ID);
        }
    }
}
