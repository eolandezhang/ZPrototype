using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.BaseInfo
{
    public class EQUIPMENT_LIST
    {
        DAL.BaseInfo.EQUIPMENT_LIST _EQUIPMENT_LIST = new DAL.BaseInfo.EQUIPMENT_LIST();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<EQUIPMENT_LIST_Entity> GetData()
        {
            return _EQUIPMENT_LIST.GetData();
        }

        public List<EQUIPMENT_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _EQUIPMENT_LIST.GetData(pageSize, pageNumber);
        }

        public List<EQUIPMENT_LIST_Entity> GetDataById(string EQUIPMENT_ID, string FACTORY_ID, string queryStr)
        {
            return _EQUIPMENT_LIST.GetDataById(EQUIPMENT_ID, FACTORY_ID, queryStr);
        }
        public List<EQUIPMENT_LIST_Entity> GetDataByFactoryId(string FACTORY_ID, string queryStr)
        {
            return _EQUIPMENT_LIST.GetDataByFactoryId(FACTORY_ID, queryStr);
        }
        public bool GetDataValidateId(string EQUIPMENT_ID, string FACTORY_ID)
        {
            return _EQUIPMENT_LIST.GetDataValidateId(EQUIPMENT_ID, FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(EQUIPMENT_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _EQUIPMENT_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(EQUIPMENT_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _EQUIPMENT_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(EQUIPMENT_LIST_Entity entity)
        {
            return _EQUIPMENT_LIST.PostDelete(entity);
        }

        #endregion



    }
}
