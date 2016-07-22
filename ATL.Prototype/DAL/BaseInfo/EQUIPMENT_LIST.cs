using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class EQUIPMENT_LIST
    {
        #region 查询

        public List<EQUIPMENT_LIST_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<EQUIPMENT_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT
        EQUIPMENT_ID,
        FACTORY_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        EQUIPMENT_NAME,
        EQUIPMENT_DESC,
        EQUIPMENT_TYPE_ID,
        EQUIPMENT_CLASS_ID,
        WORKSTATION_ID,
        ASSETMENT_ID,
        VALID_FLAG
FROM EQUIPMENT_LIST
", null);
        }

        public List<EQUIPMENT_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<EQUIPMENT_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT  TOTAL, 
        EQUIPMENT_ID,
        FACTORY_ID,
        UPDATE_USER,
        UPDATE_DATE,
        EQUIPMENT_NAME,
        EQUIPMENT_DESC,
        EQUIPMENT_TYPE_ID,
        EQUIPMENT_CLASS_ID,
        WORKSTATION_ID,
        ASSETMENT_ID,
        VALID_FLAG
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            EQUIPMENT_ID,
            FACTORY_ID,
            UPDATE_USER,
            UPDATE_DATE,
            EQUIPMENT_NAME,
            EQUIPMENT_DESC,
            EQUIPMENT_TYPE_ID,
            EQUIPMENT_CLASS_ID,
            WORKSTATION_ID,
            ASSETMENT_ID,
            VALID_FLAG
        FROM (  SELECT 
                    EQUIPMENT_ID,
                    FACTORY_ID,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    EQUIPMENT_NAME,
                    EQUIPMENT_DESC,
                    EQUIPMENT_TYPE_ID,
                    EQUIPMENT_CLASS_ID,
                    WORKSTATION_ID,
                    ASSETMENT_ID,
                    VALID_FLAG
                FROM EQUIPMENT_LIST ) T1,          
            (  SELECT COUNT (1) TOTAL FROM EQUIPMENT_LIST ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
  ", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<EQUIPMENT_LIST_Entity> GetDataById(string EQUIPMENT_ID, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<EQUIPMENT_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        EQUIPMENT_ID,
        FACTORY_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        EQUIPMENT_NAME,
        EQUIPMENT_DESC,
        EQUIPMENT_TYPE_ID,
        EQUIPMENT_CLASS_ID,
        WORKSTATION_ID,
        ASSETMENT_ID,
        VALID_FLAG
FROM EQUIPMENT_LIST
WHERE EQUIPMENT_ID=:EQUIPMENT_ID 
    AND FACTORY_ID=:FACTORY_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("EQUIPMENT_ID",OracleType.VarChar,20,EQUIPMENT_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public List<EQUIPMENT_LIST_Entity> GetDataByFactoryId(string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<EQUIPMENT_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        EQUIPMENT_ID,
        FACTORY_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        EQUIPMENT_NAME,
        EQUIPMENT_DESC,
        EQUIPMENT_TYPE_ID,
        EQUIPMENT_CLASS_ID,
        WORKSTATION_ID,
        ASSETMENT_ID,
        VALID_FLAG
FROM EQUIPMENT_LIST
WHERE FACTORY_ID=:FACTORY_ID 
" + queryStr, new[]{            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public bool GetDataValidateId(string EQUIPMENT_ID, string FACTORY_ID)
        {
            return 1 == Convert.ToInt32(OracleHelper.ExecuteScalar(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  COUNT(1)
FROM EQUIPMENT_LIST
WHERE EQUIPMENT_ID=:EQUIPMENT_ID 
    AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("EQUIPMENT_ID",OracleType.VarChar,20,EQUIPMENT_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            }));
        }

        #endregion

        #region 新增

        public int PostAdd(EQUIPMENT_LIST_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM EQUIPMENT_LIST
 WHERE EQUIPMENT_ID=:EQUIPMENT_ID AND FACTORY_ID=:FACTORY_ID
",
                new[]{
                    OracleHelper.MakeInParam("EQUIPMENT_ID",OracleType.VarChar,20,entity.EQUIPMENT_ID),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
                })
                ) > 0;
            if (exist)
            {
                return 0;
            }
            #endregion
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
INSERT INTO EQUIPMENT_LIST (
        EQUIPMENT_ID,
        FACTORY_ID,
        UPDATE_USER,
        UPDATE_DATE,
        EQUIPMENT_NAME,
        EQUIPMENT_DESC,
        EQUIPMENT_TYPE_ID,
        EQUIPMENT_CLASS_ID,
        WORKSTATION_ID,
        ASSETMENT_ID,
        VALID_FLAG
)
VALUES (
        :EQUIPMENT_ID,
        :FACTORY_ID,
        :UPDATE_USER,
        :UPDATE_DATE,
        :EQUIPMENT_NAME,
        :EQUIPMENT_DESC,
        :EQUIPMENT_TYPE_ID,
        :EQUIPMENT_CLASS_ID,
        :WORKSTATION_ID,
        :ASSETMENT_ID,
        :VALID_FLAG
)
", new[]{
            OracleHelper.MakeInParam("EQUIPMENT_ID",OracleType.VarChar,20,entity.EQUIPMENT_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("EQUIPMENT_NAME",OracleType.VarChar,20,entity.EQUIPMENT_NAME),
            OracleHelper.MakeInParam("EQUIPMENT_DESC",OracleType.VarChar,25,entity.EQUIPMENT_DESC),
            OracleHelper.MakeInParam("EQUIPMENT_TYPE_ID",OracleType.VarChar,20,entity.EQUIPMENT_TYPE_ID),
            OracleHelper.MakeInParam("EQUIPMENT_CLASS_ID",OracleType.VarChar,20,entity.EQUIPMENT_CLASS_ID),
            OracleHelper.MakeInParam("WORKSTATION_ID",OracleType.VarChar,25,entity.WORKSTATION_ID),
            OracleHelper.MakeInParam("ASSETMENT_ID",OracleType.VarChar,20,entity.ASSETMENT_ID),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(EQUIPMENT_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE EQUIPMENT_LIST SET 
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    EQUIPMENT_NAME=:EQUIPMENT_NAME,
    EQUIPMENT_DESC=:EQUIPMENT_DESC,
    EQUIPMENT_TYPE_ID=:EQUIPMENT_TYPE_ID,
    EQUIPMENT_CLASS_ID=:EQUIPMENT_CLASS_ID,
    WORKSTATION_ID=:WORKSTATION_ID,
    ASSETMENT_ID=:ASSETMENT_ID,
    VALID_FLAG=:VALID_FLAG
 WHERE 
        EQUIPMENT_ID=:EQUIPMENT_ID AND  
        FACTORY_ID=:FACTORY_ID 
", new[]{
            OracleHelper.MakeInParam("EQUIPMENT_ID",OracleType.VarChar,20,entity.EQUIPMENT_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("EQUIPMENT_NAME",OracleType.VarChar,20,entity.EQUIPMENT_NAME),
            OracleHelper.MakeInParam("EQUIPMENT_DESC",OracleType.VarChar,25,entity.EQUIPMENT_DESC),
            OracleHelper.MakeInParam("EQUIPMENT_TYPE_ID",OracleType.VarChar,20,entity.EQUIPMENT_TYPE_ID),
            OracleHelper.MakeInParam("EQUIPMENT_CLASS_ID",OracleType.VarChar,20,entity.EQUIPMENT_CLASS_ID),
            OracleHelper.MakeInParam("WORKSTATION_ID",OracleType.VarChar,25,entity.WORKSTATION_ID),
            OracleHelper.MakeInParam("ASSETMENT_ID",OracleType.VarChar,20,entity.ASSETMENT_ID),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG)
            });
        }

        #endregion

        #region 删除
        public int PostDelete(EQUIPMENT_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM EQUIPMENT_LIST 
WHERE
    EQUIPMENT_ID=:EQUIPMENT_ID AND 
    FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("EQUIPMENT_ID",OracleType.VarChar,20,entity.EQUIPMENT_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }

        #endregion



    }
}
