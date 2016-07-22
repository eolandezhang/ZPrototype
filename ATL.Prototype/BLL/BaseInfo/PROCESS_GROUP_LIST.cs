using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.BaseInfo
{
    public class PROCESS_GROUP_LIST
    {
        DAL.BaseInfo.PROCESS_GROUP_LIST _PROCESS_GROUP_LIST = new DAL.BaseInfo.PROCESS_GROUP_LIST();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<PROCESS_GROUP_LIST_Entity> GetData(string factoryId, string productTypeId, string produceProcTypeId, string queryStr)
        {
            return _PROCESS_GROUP_LIST.GetData(factoryId, productTypeId, produceProcTypeId, queryStr);
        }

        public List<PROCESS_GROUP_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _PROCESS_GROUP_LIST.GetData(pageSize, pageNumber);
        }

        #endregion

        #region 新增

        public int PostAdd(PROCESS_GROUP_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PROCESS_GROUP_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(PROCESS_GROUP_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _PROCESS_GROUP_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(PROCESS_GROUP_LIST_Entity entity)
        {
            return _PROCESS_GROUP_LIST.PostDelete(entity);
        }

        #endregion



    }
}
