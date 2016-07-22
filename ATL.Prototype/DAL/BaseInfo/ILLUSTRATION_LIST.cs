using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Web;

namespace DAL.BaseInfo
{
    public class ILLUSTRATION_LIST
    {

        #region 查询

        public List<ILLUSTRATION_LIST_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<ILLUSTRATION_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        ILLUSTRATION_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        PROCESS_ID,
        ILLUSTRATION_DESC,
        ILLUSTRATION_DATA,
        VALID_FLAG,
        IMG_LENGTH
FROM ILLUSTRATION_LIST
", null);
        }

        public List<ILLUSTRATION_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<ILLUSTRATION_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  TOTAL, 
        ILLUSTRATION_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        UPDATE_DATE,
        PROCESS_ID,
        ILLUSTRATION_DESC,
        ILLUSTRATION_DATA,
        VALID_FLAG,
        IMG_LENGTH
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            ILLUSTRATION_ID,
            FACTORY_ID,
            PRODUCT_TYPE_ID,
            PRODUCT_PROC_TYPE_ID,
            UPDATE_USER,
            UPDATE_DATE,
            PROCESS_ID,
            ILLUSTRATION_DESC,
            ILLUSTRATION_DATA,
            VALID_FLAG,
            IMG_LENGTH
        FROM (  SELECT 
                    ILLUSTRATION_ID,
                    FACTORY_ID,
                    PRODUCT_TYPE_ID,
                    PRODUCT_PROC_TYPE_ID,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    PROCESS_ID,
                    ILLUSTRATION_DESC,
                    ILLUSTRATION_DATA,
                    VALID_FLAG,
                    IMG_LENGTH
                FROM ILLUSTRATION_LIST ) T1,          
            (  SELECT COUNT (1) TOTAL FROM ILLUSTRATION_LIST ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<ILLUSTRATION_LIST_Entity> GetDataByFactoryIdAndTypeAndProcessId(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID,string PROCESS_ID)
        {
            return OracleHelper.SelectedToIList<ILLUSTRATION_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        ILLUSTRATION_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        PROCESS_ID,
        ILLUSTRATION_DESC,
        ILLUSTRATION_DATA,
        VALID_FLAG,
        IMG_LENGTH
FROM ILLUSTRATION_LIST
WHERE  FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
    AND PROCESS_ID=:PROCESS_ID
", new[]{           
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID)
            });
        }

        public IDataReader GetDataById(string ILLUSTRATION_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return OracleHelper.ExecuteReader(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        ILLUSTRATION_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        PROCESS_ID,
        ILLUSTRATION_DESC,
        ILLUSTRATION_DATA,
        VALID_FLAG,
        IMG_LENGTH
FROM ILLUSTRATION_LIST
WHERE ILLUSTRATION_ID=:ILLUSTRATION_ID 
    AND FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,25,ILLUSTRATION_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID)
            });
        }
        #endregion

        #region 新增

        public int PostAdd(ILLUSTRATION_LIST_Entity entity)
        {           

            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM ILLUSTRATION_LIST
 WHERE ILLUSTRATION_ID=:ILLUSTRATION_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
",
                new[]{
                    OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,25,entity.ILLUSTRATION_ID),
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
INSERT INTO ILLUSTRATION_LIST (
        ILLUSTRATION_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        UPDATE_DATE,
        PROCESS_ID,
        ILLUSTRATION_DESC,        
        VALID_FLAG,
        IMG_LENGTH
)
VALUES (
        :ILLUSTRATION_ID,
        :FACTORY_ID,
        :PRODUCT_TYPE_ID,
        :PRODUCT_PROC_TYPE_ID,
        :UPDATE_USER,
        :UPDATE_DATE,
        :PROCESS_ID,
        :ILLUSTRATION_DESC,        
        :VALID_FLAG,
        :IMG_LENGTH
)
", new[]{
            OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,25,entity.ILLUSTRATION_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("ILLUSTRATION_DESC",OracleType.VarChar,25,entity.ILLUSTRATION_DESC),            
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("IMG_LENGTH",OracleType.Number,entity.IMG_LENGTH)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(ILLUSTRATION_LIST_Entity entity)
        {            

            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE ILLUSTRATION_LIST SET 
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    PROCESS_ID=:PROCESS_ID,
    ILLUSTRATION_DESC=:ILLUSTRATION_DESC,    
    VALID_FLAG=:VALID_FLAG,
    IMG_LENGTH=:IMG_LENGTH
 WHERE ILLUSTRATION_ID=:ILLUSTRATION_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,25,entity.ILLUSTRATION_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("ILLUSTRATION_DESC",OracleType.VarChar,25,entity.ILLUSTRATION_DESC),            
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("IMG_LENGTH",OracleType.Number,entity.IMG_LENGTH)
            });
        }

        public int PostEdit_UploadImg(ILLUSTRATION_LIST_Entity entity)
        {
            #region 图片
            
            Byte[] bt = entity.UploadImg;
            if (bt == null)
            {
                return 0;
            }

            #endregion

            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE ILLUSTRATION_LIST SET
    ILLUSTRATION_DATA=:ILLUSTRATION_DATA
 WHERE ILLUSTRATION_ID=:ILLUSTRATION_ID 
    AND FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,25,entity.ILLUSTRATION_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID), 
            OracleHelper.MakeInParam("ILLUSTRATION_DATA",OracleType.Blob,bt)
            });
        }

        #endregion

        #region 删除
        //请移除多余的 "AND" 和 逗号
        public int PostDelete(ILLUSTRATION_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM ILLUSTRATION_LIST WHERE ILLUSTRATION_ID=:ILLUSTRATION_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,25,entity.ILLUSTRATION_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID)
            });
        }

        #endregion



    }
}
