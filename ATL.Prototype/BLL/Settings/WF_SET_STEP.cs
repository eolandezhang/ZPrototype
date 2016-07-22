using Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Settings
{
    public class WF_SET_STEP
    {
        DAL.Settings.WF_SET_STEP _WF_SET_STEP = new DAL.Settings.WF_SET_STEP();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<WF_SET_STEP_Entity> GetData()
        {
            return _WF_SET_STEP.GetData();
        }

        public List<WF_SET_STEP_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _WF_SET_STEP.GetData(pageSize, pageNumber);
        }

        public List<WF_SET_STEP_Entity> GetDataById(string WF_SET_STEP_ID, string WF_SET_NUM, string FACTORY_ID, string queryStr)
        {
            return _WF_SET_STEP.GetDataById(WF_SET_STEP_ID, WF_SET_NUM,FACTORY_ID, queryStr);
        }

        public List<WF_SET_STEP_Entity> GetDataBySetId(string WF_SET_NUM, string FACTORY_ID, string queryStr)
        {
            return _WF_SET_STEP.GetDataBySetId(WF_SET_NUM, FACTORY_ID, queryStr);
        }
       
        public bool GetDataValidateId(string WF_SET_STEP_ID, string WF_SET_NUM, string FACTORY_ID)
        {
            return _WF_SET_STEP.GetDataValidateId(WF_SET_STEP_ID, WF_SET_NUM, FACTORY_ID);
        }

        #endregion

        #region 新增

        public int PostAdd(WF_SET_STEP_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _WF_SET_STEP.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(WF_SET_STEP_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _WF_SET_STEP.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(WF_SET_STEP_Entity entity)
        {
            return _WF_SET_STEP.PostDelete(entity);
        }

        #endregion



    }
}
