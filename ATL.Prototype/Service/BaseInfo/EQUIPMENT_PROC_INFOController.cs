using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class EQUIPMENT_PROC_INFOController : ApiController
    {
        BLL.BaseInfo.EQUIPMENT_PROC_INFO _EQUIPMENT_PROC_INFO = new BLL.BaseInfo.EQUIPMENT_PROC_INFO();
        #region 查询

        public List<EQUIPMENT_PROC_INFO_Entity> GetData()
        {
            return _EQUIPMENT_PROC_INFO.GetData();
        }

        public List<EQUIPMENT_PROC_INFO_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _EQUIPMENT_PROC_INFO.GetData(pageSize, pageNumber);
        }

        public List<EQUIPMENT_PROC_INFO_Entity> GetDataByProcessIdAndTypeId(string EQUIPMENT_TYPE_ID, string PROCESS_ID, string FACTORY_ID, string queryStr)
        {
            return _EQUIPMENT_PROC_INFO.GetDataByProcessIdAndTypeId(EQUIPMENT_TYPE_ID, PROCESS_ID, FACTORY_ID, queryStr);
        }

        public List<EQUIPMENT_PROC_INFO_Entity> GetDataQuery(string PROCESS_ID, string FACTORY_ID, string EQUIPMENT_TYPE_ID, string EQUIPMENT_CLASS_ID, string EQUIPMENT_ID, string EQUIPMENT_NAME, string EQUIPMENT_DESC, string queryStr)
        {
            return _EQUIPMENT_PROC_INFO.GetDataQuery(PROCESS_ID, FACTORY_ID, EQUIPMENT_TYPE_ID, EQUIPMENT_CLASS_ID, EQUIPMENT_ID, EQUIPMENT_NAME, EQUIPMENT_DESC, queryStr);
        }

        #endregion

        #region 新增

        public int PostAdd(EQUIPMENT_PROC_INFO_Entity entity)
        {
            return _EQUIPMENT_PROC_INFO.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(EQUIPMENT_PROC_INFO_Entity entity)
        {
            return _EQUIPMENT_PROC_INFO.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(EQUIPMENT_PROC_INFO_Entity entity)
        {
            return _EQUIPMENT_PROC_INFO.PostDelete(entity);
        }

        #endregion



    }
}
