using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.Settings;
using Model.Settings;

namespace Web
{
    public partial class Site : System.Web.UI.MasterPage
    {
        Users _users = new Users();
        string _userName = Users.GetADAccount();
        UsersEntity _usersEntity = new UsersEntity();
        string _displayName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    _usersEntity = _users.GetData(_userName);
                    if (_usersEntity != null)
                    {
                        if (string.IsNullOrEmpty(_usersEntity.CNNAME))
                        {
                            _displayName = _usersEntity.USERNAME;
                            username.Text = "您好！" + _displayName;
                            lbl_description.Text = _usersEntity.DESCRIPTION;
                        }
                        else
                        {
                            _displayName = _usersEntity.CNNAME;
                            username.Text = "您好！" + _displayName;
                            username.ToolTip = _usersEntity.USERNAME;
                            lbl_description.Text = _usersEntity.DESCRIPTION;
                        }
                    }
                    else
                    {
                        //如果没有在用户表中找到该用户，则需要创建新的用户
                        _usersEntity = _users.GetADUserInfo(_userName);
                        _users.Add(_usersEntity);
                    }
                }
                else
                {
                    Response.Redirect("/AccessDenied.aspx");
                }
            }

            this.DisablePageCaching();
        }

        public void DisablePageCaching()
        {
            Response.Expires = 0;
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");
        }

        protected void SignInAsDifferentUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("/AccessDenied.aspx");

        }

    }
}