using Model.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Package
{
    public class PACKAGE_TYPE_LIST
    {
        readonly DAL.Package.PACKAGE_TYPE_LIST _PACKAGE_TYPE_LIST = new DAL.Package.PACKAGE_TYPE_LIST();
        readonly DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PACKAGE_TYPE_LIST_Entity> GetData()
        {
            return _PACKAGE_TYPE_LIST.GetData();
        }

        public List<PACKAGE_TYPE_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _PACKAGE_TYPE_LIST.GetData(pageSize, pageNumber);
        }

        public List<PACKAGE_TYPE_LIST_Entity> GetDataByFactoryId(string FACTORY_ID)
        {
            return _PACKAGE_TYPE_LIST.GetDataByFactoryId(FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PACKAGE_TYPE_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PACKAGE_TYPE_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_TYPE_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().First(x => x.USERNAME == Settings.Users.GetADAccount()).DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PACKAGE_TYPE_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_TYPE_LIST_Entity entity)
        {
            return _PACKAGE_TYPE_LIST.PostDelete(entity);
        }

        #endregion



    }
}
