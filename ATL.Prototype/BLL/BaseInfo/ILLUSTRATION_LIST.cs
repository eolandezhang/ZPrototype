using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BLL.BaseInfo
{
    public class ILLUSTRATION_LIST
    {
        DAL.BaseInfo.ILLUSTRATION_LIST _ILLUSTRATION_LIST = new DAL.BaseInfo.ILLUSTRATION_LIST();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<ILLUSTRATION_LIST_Entity> GetData()
        {
            return _ILLUSTRATION_LIST.GetData();
        }

        public List<ILLUSTRATION_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _ILLUSTRATION_LIST.GetData(pageSize, pageNumber);
        }
        public List<ILLUSTRATION_LIST_Entity> GetDataByFactoryIdAndTypeAndProcessId(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string PROCESS_ID)
        {
            return _ILLUSTRATION_LIST.GetDataByFactoryIdAndTypeAndProcessId(FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, PROCESS_ID);
        }
        public IDataReader GetDataById(string ILLUSTRATION_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return _ILLUSTRATION_LIST.GetDataById(ILLUSTRATION_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID);
        }
        #endregion

        #region 新增

        public int PostAdd(ILLUSTRATION_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _ILLUSTRATION_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(ILLUSTRATION_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _ILLUSTRATION_LIST.PostEdit(entity);
        }
        public int PostEdit_UploadImg(ILLUSTRATION_LIST_Entity entity)
        {
            return _ILLUSTRATION_LIST.PostEdit_UploadImg(entity);
        }
        #endregion

        #region 删除

        public int PostDelete(ILLUSTRATION_LIST_Entity entity)
        {
            return _ILLUSTRATION_LIST.PostDelete(entity);
        }

        #endregion



    }
}
