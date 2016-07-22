using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace Web.Package
{
    /// <summary>
    /// PACKAGE_ILLUSTRATION_INFO_ShowImg 的摘要说明
    /// </summary>
    public class PACKAGE_ILLUSTRATION_INFO_ShowImg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var PACKAGE_NO = context.Request.QueryString["PACKAGE_NO"];
                var GROUP_NO = context.Request.QueryString["GROUP_NO"];
                var VERSION_NO = context.Request.QueryString["VERSION_NO"];
                var FACTORY_ID = context.Request.QueryString["FACTORY_ID"];
                var PROCESS_ID = context.Request.QueryString["PROCESS_ID"];
                var ILLUSTRATION_ID = context.Request.QueryString["ILLUSTRATION_ID"];




                MemoryStream stream = new MemoryStream();
                IDataReader reader = new BLL.Package.PACKAGE_ILLUSTRATION_INFO().GetDataById(PACKAGE_NO, GROUP_NO, VERSION_NO, FACTORY_ID, PROCESS_ID);
                if (reader.Read())
                {
                    byte[] pic = (byte[])reader["ILLUSTRATION_DATA"];

                    stream.Write(pic, 0, pic.Length);
                    context.Response.BinaryWrite(pic);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();

                }
            }
            catch (Exception)
            {

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