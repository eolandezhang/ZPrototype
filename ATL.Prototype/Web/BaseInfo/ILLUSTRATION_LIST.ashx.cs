using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web;
using System.IO;
using System.Globalization;
using LitJson;

namespace Web.BaseInfo
{
    /// <summary>
    /// ILLUSTRATION_LIST1 的摘要说明
    /// </summary>
    public class ILLUSTRATION_LIST1 : IHttpHandler
    {
        private HttpContext context;
        public void ProcessRequest(HttpContext context)
        {
            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            extTable.Add("flash", "swf,flv");
            extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

            //最大文件大小
            int maxSize = 1000000;
            this.context = context;

            HttpPostedFile imgFile = context.Request.Files["imgFile"];

            if (imgFile == null)
            {
                showError("请选择文件。");
            }

            String dirName = context.Request.QueryString["dir"];
            String fileName = imgFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();
            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                showError("上传文件大小超过限制。");
            }
            if (String.IsNullOrEmpty(fileExt))
            {
                showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
            }

            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(imgFile.InputStream))
            {
                fileData = binaryReader.ReadBytes(imgFile.ContentLength);
            }
            var ILLUSTRATION_ID = context.Request.QueryString["ILLUSTRATION_ID"];
            var FACTORY_ID = context.Request.QueryString["FACTORY_ID"];
            var PRODUCT_TYPE_ID = context.Request.QueryString["PRODUCT_TYPE_ID"];
            var PRODUCT_PROC_TYPE_ID = context.Request.QueryString["PRODUCT_PROC_TYPE_ID"];
            var ILLUSTRATION_DESC = context.Request.QueryString["ILLUSTRATION_DESC"];
            var IMG_LENGTH = context.Request.QueryString["IMG_LENGTH"] == "" ? 0 : Convert.ToDecimal(context.Request.QueryString["IMG_LENGTH"]);
            var VALID_FLAG = context.Request.QueryString["VALID_FLAG"];
            var PROCESS_ID = context.Request.QueryString["PROCESS_ID"];
            var addOrEdit = context.Request.QueryString["addOrEdit"];
            int result;
            if (addOrEdit == "add")
            {
                result = new BLL.BaseInfo.ILLUSTRATION_LIST().PostAdd(new Model.BaseInfo.ILLUSTRATION_LIST_Entity
                {
                    ILLUSTRATION_ID = ILLUSTRATION_ID,
                    FACTORY_ID = FACTORY_ID,
                    PRODUCT_TYPE_ID = PRODUCT_TYPE_ID,
                    PRODUCT_PROC_TYPE_ID = PRODUCT_PROC_TYPE_ID,
                    ILLUSTRATION_DESC = ILLUSTRATION_DESC,
                    PROCESS_ID=PROCESS_ID,
                    IMG_LENGTH = IMG_LENGTH,
                    VALID_FLAG = VALID_FLAG
                });
                if (result > 0)
                {
                    new BLL.BaseInfo.ILLUSTRATION_LIST().PostEdit_UploadImg(new Model.BaseInfo.ILLUSTRATION_LIST_Entity
                    {
                        ILLUSTRATION_ID = ILLUSTRATION_ID,
                        FACTORY_ID = FACTORY_ID,
                        PRODUCT_TYPE_ID = PRODUCT_TYPE_ID,
                        PRODUCT_PROC_TYPE_ID = PRODUCT_PROC_TYPE_ID,
                        UploadImg = fileData
                    });
                }
            }
            else
            {
                new BLL.BaseInfo.ILLUSTRATION_LIST().PostEdit_UploadImg(new Model.BaseInfo.ILLUSTRATION_LIST_Entity
                {
                    ILLUSTRATION_ID = ILLUSTRATION_ID,
                    FACTORY_ID = FACTORY_ID,
                    PRODUCT_TYPE_ID = PRODUCT_TYPE_ID,
                    PRODUCT_PROC_TYPE_ID = PRODUCT_PROC_TYPE_ID,
                    UploadImg = fileData
                });
            }


            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = "";
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(hash));
            context.Response.End();
        }

        private void showError(string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonMapper.ToJson(hash));
            context.Response.End();
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