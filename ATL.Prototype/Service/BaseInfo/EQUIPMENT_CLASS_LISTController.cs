using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class EQUIPMENT_CLASS_LISTController : ApiController
    {
        BLL.BaseInfo.EQUIPMENT_CLASS_LIST _EQUIPMENT_CLASS_LIST = new BLL.BaseInfo.EQUIPMENT_CLASS_LIST();
        #region 查询

        public List<EQUIPMENT_CLASS_LIST_Entity> GetData()
        {
            return _EQUIPMENT_CLASS_LIST.GetData();
        }

        public List<EQUIPMENT_CLASS_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _EQUIPMENT_CLASS_LIST.GetData(pageSize, pageNumber);
        }

        public List<EQUIPMENT_CLASS_LIST_Entity> GetDataById(string EQUIPMENT_CLASS_ID, string FACTORY_ID, string queryStr)
        {
            return _EQUIPMENT_CLASS_LIST.GetDataById(EQUIPMENT_CLASS_ID, FACTORY_ID, queryStr);
        }

        public List<EQUIPMENT_CLASS_LIST_Entity> GetDataByFactoryId(string FACTORY_ID, string queryStr)
        {
            return _EQUIPMENT_CLASS_LIST.GetDataByFactoryId(FACTORY_ID, queryStr);
        }

        public bool GetDataValidateId(string EQUIPMENT_CLASS_ID, string FACTORY_ID)
        {
            return _EQUIPMENT_CLASS_LIST.GetDataValidateId(EQUIPMENT_CLASS_ID, FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(EQUIPMENT_CLASS_LIST_Entity entity)
        {
            return _EQUIPMENT_CLASS_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(EQUIPMENT_CLASS_LIST_Entity entity)
        {
            return _EQUIPMENT_CLASS_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(EQUIPMENT_CLASS_LIST_Entity entity)
        {
            return _EQUIPMENT_CLASS_LIST.PostDelete(entity);
        }

        #endregion



    }
}
