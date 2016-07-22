using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Settings
{
    public partial class Permission : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(BLL.Settings.Users.GetADAccount().ToUpper()=="ZHANG_Q"))
            {
                BLL.Settings.Permission.IsManager(this.Context);
            }
            
        }
    }
}