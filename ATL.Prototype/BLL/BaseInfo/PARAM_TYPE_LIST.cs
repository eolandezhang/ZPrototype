using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.BaseInfo
{
    public class PARAM_TYPE_LIST
    {
        DAL.BaseInfo.PARAM_TYPE_LIST _PARAM_TYPE_LIST = new DAL.BaseInfo.PARAM_TYPE_LIST();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PARAM_TYPE_LIST_Entity> GetData()
        {
            return _PARAM_TYPE_LIST.GetData();
        }

        public List<PARAM_TYPE_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _PARAM_TYPE_LIST.GetData(pageSize, pageNumber);
        }
        public List<PARAM_TYPE_LIST_Entity> GetDataType(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr){
            return _PARAM_TYPE_LIST.GetDataType(FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, queryStr);
        }
        #endregion

        #region 新增

        public int PostAdd(PARAM_TYPE_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PARAM_TYPE_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PARAM_TYPE_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PARAM_TYPE_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PARAM_TYPE_LIST_Entity entity)
        {
            return _PARAM_TYPE_LIST.PostDelete(entity);
        }

        #endregion



    }
}
