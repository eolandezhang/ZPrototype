using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.BaseInfo
{
    public class EQUIPMENT_CLASS_PROC_INFO
    {
        DAL.BaseInfo.EQUIPMENT_CLASS_PROC_INFO _EQUIPMENT_CLASS_PROC_INFO = new DAL.BaseInfo.EQUIPMENT_CLASS_PROC_INFO();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<EQUIPMENT_CLASS_PROC_INFO_Entity> GetData()
        {
            return _EQUIPMENT_CLASS_PROC_INFO.GetData();
        }

        public List<EQUIPMENT_CLASS_PROC_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _EQUIPMENT_CLASS_PROC_INFO.GetData(pageSize, pageNumber);
        }

        public List<EQUIPMENT_CLASS_PROC_INFO_Entity> GetDataById(string EQUIPMENT_CLASS_ID, string PROCESS_ID, string FACTORY_ID, string queryStr)
        {
            return _EQUIPMENT_CLASS_PROC_INFO.GetDataById(EQUIPMENT_CLASS_ID, PROCESS_ID, FACTORY_ID, queryStr);
        }

        public List<EQUIPMENT_CLASS_PROC_INFO_Entity> GetDataQuery(string EQUIPMENT_TYPE_ID, string PROCESS_ID, string FACTORY_ID, string EQUIPMENT_CLASS_ID, string EQUIPMENT_CLASS_NAME, string EQUIPMENT_CLASS_DESC, string queryStr)
        {
            return _EQUIPMENT_CLASS_PROC_INFO.GetDataQuery(EQUIPMENT_TYPE_ID, PROCESS_ID, FACTORY_ID, EQUIPMENT_CLASS_ID, EQUIPMENT_CLASS_NAME, EQUIPMENT_CLASS_DESC, queryStr);
        }

        public List<EQUIPMENT_CLASS_PROC_INFO_Entity> GetDataByProcessIdAndTypeId(string EQUIPMENT_TYPE_ID, string PROCESS_ID, string FACTORY_ID, string queryStr)
        {
            return _EQUIPMENT_CLASS_PROC_INFO.GetDataByProcessIdAndTypeId(EQUIPMENT_TYPE_ID, PROCESS_ID, FACTORY_ID, queryStr);
        }


        #endregion

        #region 新增

        public int PostAdd(EQUIPMENT_CLASS_PROC_INFO_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _EQUIPMENT_CLASS_PROC_INFO.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(EQUIPMENT_CLASS_PROC_INFO_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _EQUIPMENT_CLASS_PROC_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(EQUIPMENT_CLASS_PROC_INFO_Entity entity)
        {
            return _EQUIPMENT_CLASS_PROC_INFO.PostDelete(entity);
        }

        #endregion



    }
}
