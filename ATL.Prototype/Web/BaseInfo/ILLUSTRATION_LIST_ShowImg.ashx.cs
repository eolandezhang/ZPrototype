using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace Web.BaseInfo
{
    /// <summary>
    /// ILLUSTRATION_LIST_ShowImg 的摘要说明
    /// </summary>
    public class ILLUSTRATION_LIST_ShowImg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var ILLUSTRATION_ID = context.Request.QueryString["ILLUSTRATION_ID"];
                var FACTORY_ID = context.Request.QueryString["FACTORY_ID"];
                var PRODUCT_TYPE_ID = context.Request.QueryString["PRODUCT_TYPE_ID"];
                var PRODUCT_PROC_TYPE_ID = context.Request.QueryString["PRODUCT_PROC_TYPE_ID"];

                MemoryStream stream = new MemoryStream();
                IDataReader reader = new BLL.BaseInfo.ILLUSTRATION_LIST().GetDataById(ILLUSTRATION_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
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