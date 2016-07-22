using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Package
{
    public partial class PACKAGE_TYPE_LIST : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.Settings.Permission.IsManager(this.Context);
        }
    }
}