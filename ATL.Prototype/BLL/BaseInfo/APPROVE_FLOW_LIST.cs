using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.BaseInfo
{
    public class APPROVE_FLOW_LIST
    {
        DAL.BaseInfo.APPROVE_FLOW_LIST _APPROVE_FLOW_LIST = new DAL.BaseInfo.APPROVE_FLOW_LIST();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<APPROVE_FLOW_LIST_Entity> GetData()
        {
            return _APPROVE_FLOW_LIST.GetData();
        }

        public List<APPROVE_FLOW_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _APPROVE_FLOW_LIST.GetData(pageSize, pageNumber);
        }

        public List<APPROVE_FLOW_LIST_Entity> GetDataByFactoryId(string FACTORY_ID)
        {
            return _APPROVE_FLOW_LIST.GetDataByFactoryId(FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(APPROVE_FLOW_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _APPROVE_FLOW_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(APPROVE_FLOW_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _APPROVE_FLOW_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(APPROVE_FLOW_LIST_Entity entity)
        {
            return _APPROVE_FLOW_LIST.PostDelete(entity);
        }

        #endregion



    }
}
