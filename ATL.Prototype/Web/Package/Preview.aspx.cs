using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Package
{
    public partial class Preview : System.Web.UI.Page
    {
        BLL.Package.PACKAGE_BASE_INFO package = new BLL.Package.PACKAGE_BASE_INFO();

        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                string packageNo = Request.QueryString["packageNo"].ToString();
                string factoryId = Request.QueryString["factoryId"].ToString();
                string versionNo = Request.QueryString["versionNo"].ToString();               

                cover.InnerHtml = package.Cover(packageNo, versionNo, factoryId);
                parameter.InnerHtml = package.Parameter(packageNo, versionNo, factoryId);
                material.InnerHtml = package.Material(packageNo, versionNo, factoryId);
                equipment.InnerHtml = package.Equipment(packageNo, versionNo, factoryId);
                illustration.InnerHtml = package.Illustration(packageNo, versionNo, factoryId);
                bom.InnerHtml = package.Bom(packageNo, versionNo, factoryId);
            }

        }
    }
}