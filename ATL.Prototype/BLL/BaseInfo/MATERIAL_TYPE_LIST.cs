﻿using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.BaseInfo
{
    public class MATERIAL_TYPE_LIST
    {
        DAL.BaseInfo.MATERIAL_TYPE_LIST _MATERIAL_TYPE_LIST = new DAL.BaseInfo.MATERIAL_TYPE_LIST();
        DAL.Settings.Users _Users = new DAL.Settings.Users();
        #region 查询

        public List<MATERIAL_TYPE_LIST_Entity> GetData()
        {
            return _MATERIAL_TYPE_LIST.GetData();
        }

        public List<MATERIAL_TYPE_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return _MATERIAL_TYPE_LIST.GetData(pageSize, pageNumber);
        }

        public List<MATERIAL_TYPE_LIST_Entity> GetDataByType(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return _MATERIAL_TYPE_LIST.GetDataByType(FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, queryStr);
        }

        public List<MATERIAL_TYPE_LIST_Entity> GetDataByCategoryId(string MATERIAL_CATEGORY_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return _MATERIAL_TYPE_LIST.GetDataByCategoryId(MATERIAL_CATEGORY_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, queryStr);
        }

        public List<MATERIAL_TYPE_LIST_Entity> GetDataById(string MATERIAL_TYPE_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return _MATERIAL_TYPE_LIST.GetDataById(MATERIAL_TYPE_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, queryStr);
        }

        #endregion

        #region 新增

        public int PostAdd(MATERIAL_TYPE_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _MATERIAL_TYPE_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(MATERIAL_TYPE_LIST_Entity entity)
        {
            entity.UPDATE_USER = _Users.GetData().Where(x => x.USERNAME == BLL.Settings.Users.GetADAccount()).First().DESCRIPTION;
            entity.UPDATE_DATE = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return _MATERIAL_TYPE_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(MATERIAL_TYPE_LIST_Entity entity)
        {
            return _MATERIAL_TYPE_LIST.PostDelete(entity);
        }

        #endregion



    }
}
