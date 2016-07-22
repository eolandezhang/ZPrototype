using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.BaseInfo
{
    public class PRODUCT_TYPE_LIST
    {
        DAL.BaseInfo.PRODUCT_TYPE_LIST _PRODUCT_TYPE_LIST = new DAL.BaseInfo.PRODUCT_TYPE_LIST();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PRODUCT_TYPE_LIST_Entity> GetData()
        {
            return _PRODUCT_TYPE_LIST.GetData();
        }

        public List<PRODUCT_TYPE_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _PRODUCT_TYPE_LIST.GetData(pageSize, pageNumber);
        }

        public List<PRODUCT_TYPE_LIST_Entity> GetDataByFactoryId(string FACTORY_ID)
        {
            return _PRODUCT_TYPE_LIST.GetDataByFactoryId(FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PRODUCT_TYPE_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PRODUCT_TYPE_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PRODUCT_TYPE_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PRODUCT_TYPE_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PRODUCT_TYPE_LIST_Entity entity)
        {
            return _PRODUCT_TYPE_LIST.PostDelete(entity);
        }

        #endregion



    }
}
