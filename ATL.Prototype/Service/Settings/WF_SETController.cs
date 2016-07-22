using Model.Settings;
using BLL.Settings;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.Settings
{
    public class WF_SETController : ApiController
    {
        BLL.Settings.WF_SET _WF_SET = new BLL.Settings.WF_SET();
        #region 查询

        public List<WF_SET_Entity> GetData()
        {
            return _WF_SET.GetData();
        }

        public List<WF_SET_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _WF_SET.GetData(pageSize, pageNumber);
        }

        public List<WF_SET_Entity> GetDataById(string WF_SET_NUM, string FACTORY_ID, string queryStr)
        {
            return _WF_SET.GetDataById(WF_SET_NUM, FACTORY_ID, queryStr);
        }
        public List<WF_SET_Entity> GetDataByFactoryId(string FACTORY_ID, string queryStr)
        {
            return _WF_SET.GetDataByFactoryId(FACTORY_ID, queryStr);
        }
        public bool GetDataValidateId(string WF_SET_NUM, string FACTORY_ID)
        {
            return _WF_SET.GetDataValidateId(WF_SET_NUM, FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(WF_SET_Entity entity)
        {
            return _WF_SET.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(WF_SET_Entity entity)
        {
            return _WF_SET.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(WF_SET_Entity entity)
        {
            return _WF_SET.PostDelete(entity);
        }

        #endregion



    }
}
