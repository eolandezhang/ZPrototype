﻿using Model.BaseInfo;
using BLL.BaseInfo;
using System.Collections.Generic;
using System.Web.Http;
namespace Service.BaseInfo
{
    public class MATERIAL_PN_LISTController : ApiController
    {
        BLL.BaseInfo.MATERIAL_PN_LIST _MATERIAL_PN_LIST = new BLL.BaseInfo.MATERIAL_PN_LIST();
        #region 查询

        public List<MATERIAL_PN_LIST_Entity> GetData()
        {
            return _MATERIAL_PN_LIST.GetData();
        }

        public List<MATERIAL_PN_LIST_Entity> GetDataPage(decimal pageSize, decimal pageNumber)
        {
            return _MATERIAL_PN_LIST.GetData(pageSize, pageNumber);
        }

        public List<MATERIAL_PN_LIST_Entity> GetDataByCategoryId(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string MATERIAL_CATEGORY_ID)
        {
            return _MATERIAL_PN_LIST.GetDataByCategoryId(FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, MATERIAL_CATEGORY_ID);
        }

        public bool GetDataValidateId(string MATERIAL_PN_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string MATERIAL_CATEGORY_ID)
        {
            return _MATERIAL_PN_LIST.GetDataValidateId(MATERIAL_PN_ID, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, MATERIAL_CATEGORY_ID);
        }

        public List<MATERIAL_PN_LIST_Entity> GetDataById(string MATERIAL_PN, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return _MATERIAL_PN_LIST.GetDataById(MATERIAL_PN, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, queryStr);
        }

        public List<MATERIAL_PN_LIST_Entity> GetDataByType(string FACTORY_ID, string MATERIAL_TYPE_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return _MATERIAL_PN_LIST.GetDataByType(FACTORY_ID, MATERIAL_TYPE_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, queryStr);
        }

        public List<MATERIAL_PN_LIST_Entity> GetDataQuery(string MATERIAL_TYPE_GRP_NUM, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string MATERIAL_TYPE_ID, string MATERIAL_PN_ID, string MATERIAL_PN_NAME, string MATERIAL_PN_DESC, string queryStr)
        {
            return _MATERIAL_PN_LIST.GetDataQuery(MATERIAL_TYPE_GRP_NUM, FACTORY_ID, PRODUCT_TYPE_ID, PRODUCT_PROC_TYPE_ID, MATERIAL_TYPE_ID, MATERIAL_PN_ID, MATERIAL_PN_NAME, MATERIAL_PN_DESC, queryStr);
        }

        #endregion

        #region 新增

        public int PostAdd(MATERIAL_PN_LIST_Entity entity)
        {
            return _MATERIAL_PN_LIST.PostAdd(entity);
        }

        #endregion

        #region 修改

        public int PostEdit(MATERIAL_PN_LIST_Entity entity)
        {
            return _MATERIAL_PN_LIST.PostEdit(entity);
        }

        #endregion

        #region 删除

        public int PostDelete(MATERIAL_PN_LIST_Entity entity)
        {
            return _MATERIAL_PN_LIST.PostDelete(entity);
        }

        #endregion



    }
}
