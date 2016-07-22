using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Package
{
    public partial class PACKAGE_FLOW_INFO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.Settings.Permission.IsViewer(this.Context);
        }
    }
}