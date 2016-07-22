using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class EQUIPMENT_TYPE_LISTController : ApiController
    {
        BLL.BaseInfo.EQUIPMENT_TYPE_LIST _EQUIPMENT_TYPE_LIST = new BLL.BaseInfo.EQUIPMENT_TYPE_LIST();
        #region 查询

        public List<EQUIPMENT_TYPE_LIST_Entity> GetData()
        {
            return _EQUIPMENT_TYPE_LIST.GetData();
        }

        public List<EQUIPMENT_TYPE_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _EQUIPMENT_TYPE_LIST.GetData(pageSize, pageNumber);
        }

        public List<EQUIPMENT_TYPE_LIST_Entity> GetDataById(string EQUIPMENT_TYPE_ID, string FACTORY_ID, string queryStr)
        {
            return _EQUIPMENT_TYPE_LIST.GetDataById(EQUIPMENT_TYPE_ID, FACTORY_ID, queryStr);
        }

        public List<EQUIPMENT_TYPE_LIST_Entity> GetDataByFactoryId(string FACTORY_ID, string queryStr)
        {
            return _EQUIPMENT_TYPE_LIST.GetDataByFactoryId(FACTORY_ID, queryStr);
        }

        #endregion

        #region 新增

        public int PostAdd(EQUIPMENT_TYPE_LIST_Entity entity)
        {
            return _EQUIPMENT_TYPE_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(EQUIPMENT_TYPE_LIST_Entity entity)
        {
            return _EQUIPMENT_TYPE_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(EQUIPMENT_TYPE_LIST_Entity entity)
        {
            return _EQUIPMENT_TYPE_LIST.PostDelete(entity);
        }

        #endregion



    }
}
