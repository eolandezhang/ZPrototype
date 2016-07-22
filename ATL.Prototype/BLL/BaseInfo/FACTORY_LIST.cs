using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.BaseInfo
{
    public class FACTORY_LIST
    {
        DAL.BaseInfo.FACTORY_LIST _FACTORY_LIST = new DAL.BaseInfo.FACTORY_LIST();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<FACTORY_LIST_Entity> GetData()
        {
            return _FACTORY_LIST.GetData();
        }

        public List<FACTORY_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _FACTORY_LIST.GetData(pageSize, pageNumber);
        }

        #endregion

        #region 新增

        public int PostAdd(FACTORY_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _FACTORY_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(FACTORY_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
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
