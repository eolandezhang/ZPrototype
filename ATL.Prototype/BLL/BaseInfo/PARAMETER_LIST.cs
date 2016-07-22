using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.BaseInfo
{
    public class PARAMETER_LIST
    {
        DAL.BaseInfo.PARAMETER_LIST _PARAMETER_LIST = new DAL.BaseInfo.PARAMETER_LIST();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PARAMETER_LIST_Entity> GetData()
        {
            return _PARAMETER_LIST.GetData();
        }

        public List<PARAMETER_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _PARAMETER_LIST.GetData(pageSize, pageNumber);
        }

        public List<PARAMETER_LIST_Entity> GetDataByType(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return _PARAMETER_LIST.GetDataByType(FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
        }

        public List<PARAMETER_LIST_Entity> GetDataByPType(string PARAM_TYPE_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return _PARAMETER_LIST.GetDataByPType(PARAM_TYPE_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, queryStr);
        }

        public List<PARAMETER_LIST_Entity> GetDataById(string PARAMETER_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return _PARAMETER_LIST.GetDataById(PARAMETER_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(PARAMETER_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PARAMETER_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PARAMETER_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PARAMETER_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PARAMETER_LIST_Entity entity)
        {
            return _PARAMETER_LIST.PostDelete(entity);
        }

        #endregion



    }
}
