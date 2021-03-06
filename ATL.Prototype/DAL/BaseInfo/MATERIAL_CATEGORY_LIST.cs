﻿using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class MATERIAL_CATEGORY_LIST
    {
        #region 查询

        public List<MATERIAL_CATEGORY_LIST_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<MATERIAL_CATEGORY_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT
        MATERIAL_CATEGORY_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        MATERIAL_CATEGORY_NAME,
        MATERIAL_CATEGORY_DESC,
        VALID_FLAG
FROM MATERIAL_CATEGORY_LIST
", null);
        }

        public List<MATERIAL_CATEGORY_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<MATERIAL_CATEGORY_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT  TOTAL, 
        MATERIAL_CATEGORY_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        UPDATE_DATE,
        MATERIAL_CATEGORY_NAME,
        MATERIAL_CATEGORY_DESC,
        VALID_FLAG
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            MATERIAL_CATEGORY_ID,
            FACTORY_ID,
            PRODUCT_TYPE_ID,
            PRODUCT_PROC_TYPE_ID,
            UPDATE_USER,
            UPDATE_DATE,
            MATERIAL_CATEGORY_NAME,
            MATERIAL_CATEGORY_DESC,
            VALID_FLAG
        FROM (  SELECT 
                    MATERIAL_CATEGORY_ID,
                    FACTORY_ID,
                    PRODUCT_TYPE_ID,
                    PRODUCT_PROC_TYPE_ID,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    MATERIAL_CATEGORY_NAME,
                    MATERIAL_CATEGORY_DESC,
                    VALID_FLAG
                FROM MATERIAL_CATEGORY_LIST ) T1,          
            (  SELECT COUNT (1) TOTAL FROM MATERIAL_CATEGORY_LIST ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
  ", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<MATERIAL_CATEGORY_LIST_Entity> GetDataById(string MATERIAL_CATEGORY_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<MATERIAL_CATEGORY_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        MATERIAL_CATEGORY_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        MATERIAL_CATEGORY_NAME,
        MATERIAL_CATEGORY_DESC,
        VALID_FLAG
FROM MATERIAL_CATEGORY_LIST
WHERE MATERIAL_CATEGORY_ID=:MATERIAL_CATEGORY_ID 
    AND FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("MATERIAL_CATEGORY_ID",OracleType.VarChar,20,MATERIAL_CATEGORY_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID)
            });
        }

        public List<MATERIAL_CATEGORY_LIST_Entity> GetDataByFactoryIdAndTypeId(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<MATERIAL_CATEGORY_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        MATERIAL_CATEGORY_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        MATERIAL_CATEGORY_NAME,
        MATERIAL_CATEGORY_DESC,
        VALID_FLAG
FROM MATERIAL_CATEGORY_LIST
WHERE  FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
" + queryStr, new[]{            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID)
            });
        }

        public bool GetDataValidateId(string MATERIAL_CATEGORY_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return 1 == Convert.ToInt32(OracleHelper.ExecuteScalar(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  COUNT(1)
FROM MATERIAL_CATEGORY_LIST
WHERE MATERIAL_CATEGORY_ID=:MATERIAL_CATEGORY_ID 
    AND FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("MATERIAL_CATEGORY_ID",OracleType.VarChar,20,MATERIAL_CATEGORY_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID)
            }));
        }

        #endregion

        #region 新增

        public int PostAdd(MATERIAL_CATEGORY_LIST_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM MATERIAL_CATEGORY_LIST
 WHERE MATERIAL_CATEGORY_ID=:MATERIAL_CATEGORY_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
",
                new[]{
                    OracleHelper.MakeInParam("MATERIAL_CATEGORY_ID",OracleType.VarChar,20,entity.MATERIAL_CATEGORY_ID),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
                    OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
                    OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID)
                })
                ) > 0;
            if (exist)
            {
                return 0;
            }
            #endregion
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
INSERT INTO MATERIAL_CATEGORY_LIST (
        MATERIAL_CATEGORY_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        UPDATE_DATE,
        MATERIAL_CATEGORY_NAME,
        MATERIAL_CATEGORY_DESC,
        VALID_FLAG
)
VALUES (
        :MATERIAL_CATEGORY_ID,
        :FACTORY_ID,
        :PRODUCT_TYPE_ID,
        :PRODUCT_PROC_TYPE_ID,
        :UPDATE_USER,
        :UPDATE_DATE,
        :MATERIAL_CATEGORY_NAME,
        :MATERIAL_CATEGORY_DESC,
        :VALID_FLAG
)
", new[]{
            OracleHelper.MakeInParam("MATERIAL_CATEGORY_ID",OracleType.VarChar,20,entity.MATERIAL_CATEGORY_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("MATERIAL_CATEGORY_NAME",OracleType.VarChar,50,entity.MATERIAL_CATEGORY_NAME),
            OracleHelper.MakeInParam("MATERIAL_CATEGORY_DESC",OracleType.VarChar,50,entity.MATERIAL_CATEGORY_DESC),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(MATERIAL_CATEGORY_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE MATERIAL_CATEGORY_LIST SET 
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    MATERIAL_CATEGORY_NAME=:MATERIAL_CATEGORY_NAME,
    MATERIAL_CATEGORY_DESC=:MATERIAL_CATEGORY_DESC,
    VALID_FLAG=:VALID_FLAG
 WHERE 
        MATERIAL_CATEGORY_ID=:MATERIAL_CATEGORY_ID AND  
        FACTORY_ID=:FACTORY_ID AND  
        PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND  
        PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
", new[]{
            OracleHelper.MakeInParam("MATERIAL_CATEGORY_ID",OracleType.VarChar,20,entity.MATERIAL_CATEGORY_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("MATERIAL_CATEGORY_NAME",OracleType.VarChar,50,entity.MATERIAL_CATEGORY_NAME),
            OracleHelper.MakeInParam("MATERIAL_CATEGORY_DESC",OracleType.VarChar,50,entity.MATERIAL_CATEGORY_DESC),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG)
            });
        }

        #endregion

        #region 删除
        public int PostDelete(MATERIAL_CATEGORY_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM MATERIAL_CATEGORY_LIST 
WHERE
    MATERIAL_CATEGORY_ID=:MATERIAL_CATEGORY_ID AND 
    FACTORY_ID=:FACTORY_ID AND 
    PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND 
    PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("MATERIAL_CATEGORY_ID",OracleType.VarChar,20,entity.MATERIAL_CATEGORY_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID)
            });
        }

        #endregion



    }
}
