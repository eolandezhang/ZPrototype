using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class FACTORY_LISTController : ApiController
    {
        BLL.BaseInfo.FACTORY_LIST _FACTORY_LIST = new BLL.BaseInfo.FACTORY_LIST();
        #region 查询

        public List<FACTORY_LIST_Entity> GetData()
        {
            return _FACTORY_LIST.GetData();
        }

        public List<FACTORY_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _FACTORY_LIST.GetData(pageSize, pageNumber);
        }

        #endregion

        #region 新增

        public int PostAdd(FACTORY_LIST_Entity entity)
        {
            return _FACTORY_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(FACTORY_LIST_Entity entity)
        {
            return _FACTORY_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(FACTORY_LIST_Entity entity)
        {
            return _FACTORY_LIST.PostDelete(entity);
        }

        #endregion



    }
}
