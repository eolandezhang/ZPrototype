using Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Settings
{
    public class Permission
    {
        readonly DAL.Settings.Permission _permission = new DAL.Settings.Permission();
        //DAL.Package.PACKAGE_BASE_INFO _package = new DAL.Package.PACKAGE_BASE_INFO();
        public bool CheckRight(string PMES_USER_ID, string FACTORY_ID, string PMES_TASK_ID)
        {
            return _permission.CheckGroupRight(PMES_USER_ID, FACTORY_ID, PMES_TASK_ID) || _permission.CheckTaskRight(PMES_USER_ID, FACTORY_ID, PMES_TASK_ID);
        }

        //修改，删除，导出
        public static bool CheckPackageRight(string PACKAGE_NO, string FACTORY_ID, string VERSION_NO)
        {
            var currentUser = new Users().GetCurrentUser();
            var x = new DAL.Package.PACKAGE_BASE_INFO().GetDataById(PACKAGE_NO, FACTORY_ID, VERSION_NO, "");
            if (!x.Any()) return false;
            var package = x.First();
            //草稿，已退回
            return ((package.STATUS == "1" || package.STATUS == "3") && (package.PREPARED_BY == currentUser.DESCRIPTION));
        }

        //开启审批流程的条件
        //是文件的制作人
        public static bool CheckStartWFRight(string PACKAGE_NO, string FACTORY_ID, string VERSION_NO)
        {
            var currentUser = new Users().GetCurrentUser();
            var x = new DAL.Package.PACKAGE_BASE_INFO().GetDataById(PACKAGE_NO, FACTORY_ID, VERSION_NO, "");
            if (!x.Any()) return false;
            var package = x.First();
            return (package.PREPARED_BY == currentUser.DESCRIPTION);
        }

        //新增，复制
        public static bool IsEditor()
        {
            var currentUser = new Users().GetCurrentUser();
            return new Permission().CheckRight(currentUser.DESCRIPTION, currentUser.FACTORY_ID, Param.TASK.pkgEditor.ToString("g"));
        }


        //后台管理
        public static void IsManager(HttpContext context)
        {
            var currentUser = new Users().GetCurrentUser();
            if (!new Permission().CheckRight(currentUser.DESCRIPTION, currentUser.FACTORY_ID, Param.TASK.pkgSetting.ToString("g")))
            {
                context.Response.Redirect("/NoPermission.aspx");
            }
        }
        public static bool IsManager()
        {
            var currentUser = new Users().GetCurrentUser();
            return new Permission().CheckRight(currentUser.DESCRIPTION, currentUser.FACTORY_ID, Param.TASK.pkgSetting.ToString("g"));
        }
        //查看文件
        public static void IsViewer(HttpContext context)
        {
            var currentUser = new Users().GetCurrentUser();
            if (!new Permission().CheckRight(currentUser.DESCRIPTION, currentUser.FACTORY_ID, Param.TASK.pkgView.ToString("g")))
            {
                context.Response.Redirect("/NoPermission.aspx");
            }
        }
        //导出文件
        public static bool IsExporter()
        {
            var currentUser = new Users().GetCurrentUser();
            //有pkgExport功能的就可以，无论文件是什么状态。
            return new Permission().CheckRight(currentUser.DESCRIPTION, currentUser.FACTORY_ID, Param.TASK.pkgExport.ToString("g"));
        }
    }
}
