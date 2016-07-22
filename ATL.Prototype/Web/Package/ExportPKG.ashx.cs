using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;

namespace Web.Package
{
    /// <summary>
    /// ExportPKG 的摘要说明
    /// </summary>
    public class ExportPKG : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string PACKAGE_NO = HttpContext.Current.Request.QueryString["PACKAGE_NO"];
            string FACTORY_ID = HttpContext.Current.Request.QueryString["FACTORY_ID"];
            string VERSION_NO = HttpContext.Current.Request.QueryString["VERSION_NO"];
            int result=new BLL.Package.PACKAGE_BASE_INFO().Export(PACKAGE_NO, FACTORY_ID, VERSION_NO);
            if (result==-1)
            {
                if (HttpContext.Current.Request.UrlReferrer != null)
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.UrlReferrer.ToString());
                }
            }
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}