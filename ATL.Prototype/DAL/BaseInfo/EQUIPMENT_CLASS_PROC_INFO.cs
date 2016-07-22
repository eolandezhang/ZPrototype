using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class EQUIPMENT_CLASS_PROC_INFO
    {
        #region 查询

        public List<EQUIPMENT_CLASS_PROC_INFO_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<EQUIPMENT_CLASS_PROC_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT
        PROCESS_ID,
        EQUIPMENT_TYPE_ID,
        EQUIPMENT_CLASS_ID,
        FACTORY_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        REMARK
FROM EQUIPMENT_CLASS_PROC_INFO
", null);
        }

        public List<EQUIPMENT_CLASS_PROC_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<EQUIPMENT_CLASS_PROC_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT  TOTAL, 
        PROCESS_ID,
        EQUIPMENT_TYPE_ID,
        EQUIPMENT_CLASS_ID,
        FACTORY_ID,
        UPDATE_USER,
        UPDATE_DATE,
        REMARK
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            PROCESS_ID,
            EQUIPMENT_TYPE_ID,
            EQUIPMENT_CLASS_ID,
            FACTORY_ID,
            UPDATE_USER,
            UPDATE_DATE,
            REMARK
        FROM (  SELECT 
                    PROCESS_ID,
                    EQUIPMENT_TYPE_ID,
                    EQUIPMENT_CLASS_ID,
                    FACTORY_ID,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    REMARK
                FROM EQUIPMENT_CLASS_PROC_INFO ) T1,          
            (  SELECT COUNT (1) TOTAL FROM EQUIPMENT_CLASS_PROC_INFO ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
  ", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<EQUIPMENT_CLASS_PROC_INFO_Entity> GetDataById(string EQUIPMENT_CLASS_ID, string PROCESS_ID, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<EQUIPMENT_CLASS_PROC_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PROCESS_ID,
        EQUIPMENT_TYPE_ID,
        EQUIPMENT_CLASS_ID,
        FACTORY_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        REMARK
FROM EQUIPMENT_CLASS_PROC_INFO
WHERE EQUIPMENT_CLASS_ID=:EQUIPMENT_CLASS_ID     
    AND PROCESS_ID=:PROCESS_ID 
    AND FACTORY_ID=:FACTORY_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("EQUIPMENT_CLASS_ID",OracleType.VarChar,20,EQUIPMENT_CLASS_ID),            
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,20,PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public List<EQUIPMENT_CLASS_PROC_INFO_Entity> GetDataByProcessIdAndTypeId(string EQUIPMENT_TYPE_ID, string PROCESS_ID, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<EQUIPMENT_CLASS_PROC_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.PROCESS_ID,
       A.EQUIPMENT_TYPE_ID,
       A.EQUIPMENT_CLASS_ID,
       A.FACTORY_ID,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       A.REMARK,
       B.EQUIPMENT_CLASS_DESC
  FROM EQUIPMENT_CLASS_PROC_INFO A, EQUIPMENT_CLASS_LIST B
 WHERE     A.EQUIPMENT_TYPE_ID = :EQUIPMENT_TYPE_ID
       AND A.PROCESS_ID = :PROCESS_ID
       AND A.FACTORY_ID = :FACTORY_ID
       AND B.EQUIPMENT_CLASS_ID = A.EQUIPMENT_CLASS_ID
       AND B.FACTORY_ID = A.FACTORY_ID
" + queryStr, new[]{            
            OracleHelper.MakeInParam("EQUIPMENT_TYPE_ID",OracleType.VarChar,20,EQUIPMENT_TYPE_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,20,PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }

        public List<EQUIPMENT_CLASS_PROC_INFO_Entity> GetDataQuery(string EQUIPMENT_TYPE_ID, string PROCESS_ID, string FACTORY_ID, string EQUIPMENT_CLASS_ID, string EQUIPMENT_CLASS_NAME, string EQUIPMENT_CLASS_DESC, string queryStr)
        {
            var str1 = string.Empty;
            var str2 = string.Empty;
            var str3 = string.Empty;
            List<OracleParameter> paramList = new List<OracleParameter>{
                OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,20,PROCESS_ID),
                OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            };
            if (!string.IsNullOrEmpty(EQUIPMENT_TYPE_ID))
            {
                str1 = " AND A.EQUIPMENT_TYPE_ID = :EQUIPMENT_TYPE_ID ";
                paramList.Add(OracleHelper.MakeInParam("EQUIPMENT_TYPE_ID", OracleType.VarChar, 20, EQUIPMENT_TYPE_ID));
            }
            if (!string.IsNullOrEmpty(EQUIPMENT_CLASS_ID))
            {
                str2 = " AND A.EQUIPMENT_CLASS_ID LIKE '%' || :EQUIPMENT_CLASS_ID || '%' ";
                paramList.Add(OracleHelper.MakeInParam("EQUIPMENT_CLASS_ID", OracleType.VarChar, EQUIPMENT_CLASS_ID));
            }
            if (!string.IsNullOrEmpty(EQUIPMENT_CLASS_NAME))
            {
                str3 = " AND B.EQUIPMENT_CLASS_NAME LIKE '%' || :EQUIPMENT_CLASS_NAME || '%' ";
                paramList.Add(OracleHelper.MakeInParam("EQUIPMENT_CLASS_NAME", OracleType.VarChar, EQUIPMENT_CLASS_NAME));
            }
            if (!string.IsNullOrEmpty(EQUIPMENT_CLASS_DESC))
            {
                str3 = " AND B.EQUIPMENT_CLASS_DESC LIKE '%' || :EQUIPMENT_CLASS_DESC || '%' ";
                paramList.Add(OracleHelper.MakeInParam("EQUIPMENT_CLASS_DESC", OracleType.VarChar, EQUIPMENT_CLASS_DESC));
            }
            string str = str1 + str2 + str3;
            OracleParameter[] param = paramList.ToArray();
            return OracleHelper.SelectedToIList<EQUIPMENT_CLASS_PROC_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.PROCESS_ID,
       A.EQUIPMENT_TYPE_ID,
       A.EQUIPMENT_CLASS_ID,
       A.FACTORY_ID,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       A.REMARK,
       B.EQUIPMENT_CLASS_NAME,
       B.EQUIPMENT_CLASS_DESC
  FROM EQUIPMENT_CLASS_PROC_INFO A, EQUIPMENT_CLASS_LIST B
 WHERE     A.PROCESS_ID = :PROCESS_ID
       AND A.FACTORY_ID = :FACTORY_ID       
       AND B.EQUIPMENT_CLASS_ID = A.EQUIPMENT_CLASS_ID
       AND B.FACTORY_ID = A.FACTORY_ID
" + str + queryStr, param);
        }
        #endregion

        #region 新增

        public int PostAdd(EQUIPMENT_CLASS_PROC_INFO_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM EQUIPMENT_CLASS_PROC_INFO
 WHERE EQUIPMENT_CLASS_ID=:EQUIPMENT_CLASS_ID AND PROCESS_ID=:PROCESS_ID AND FACTORY_ID=:FACTORY_ID
",
                new[]{
                    OracleHelper.MakeInParam("EQUIPMENT_CLASS_ID",OracleType.VarChar,20,entity.EQUIPMENT_CLASS_ID),
                    OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,20,entity.PROCESS_ID),
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
INSERT INTO EQUIPMENT_CLASS_PROC_INFO (
        PROCESS_ID,
        EQUIPMENT_TYPE_ID,
        EQUIPMENT_CLASS_ID,
        FACTORY_ID,
        UPDATE_USER,
        UPDATE_DATE,
        REMARK
)
VALUES (
        :PROCESS_ID,
        :EQUIPMENT_TYPE_ID,
        :EQUIPMENT_CLASS_ID,
        :FACTORY_ID,
        :UPDATE_USER,
        :UPDATE_DATE,
        :REMARK
)
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,20,entity.PROCESS_ID),
            OracleHelper.MakeInParam("EQUIPMENT_TYPE_ID",OracleType.VarChar,20,entity.EQUIPMENT_TYPE_ID),
            OracleHelper.MakeInParam("EQUIPMENT_CLASS_ID",OracleType.VarChar,20,entity.EQUIPMENT_CLASS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("REMARK",OracleType.VarChar,25,entity.REMARK)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(EQUIPMENT_CLASS_PROC_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE EQUIPMENT_CLASS_PROC_INFO SET 
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    REMARK=:REMARK,
    EQUIPMENT_TYPE_ID=:EQUIPMENT_TYPE_ID
 WHERE 
        EQUIPMENT_CLASS_ID=:EQUIPMENT_CLASS_ID AND          
        PROCESS_ID=:PROCESS_ID AND  
        FACTORY_ID=:FACTORY_ID 
", new[]{
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,20,entity.PROCESS_ID),
            OracleHelper.MakeInParam("EQUIPMENT_TYPE_ID",OracleType.VarChar,20,entity.EQUIPMENT_TYPE_ID),
            OracleHelper.MakeInParam("EQUIPMENT_CLASS_ID",OracleType.VarChar,20,entity.EQUIPMENT_CLASS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("REMARK",OracleType.VarChar,25,entity.REMARK)
            });
        }

        #endregion

        #region 删除
        public int PostDelete(EQUIPMENT_CLASS_PROC_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM EQUIPMENT_CLASS_PROC_INFO 
WHERE
    EQUIPMENT_CLASS_ID=:EQUIPMENT_CLASS_ID AND     
    PROCESS_ID=:PROCESS_ID AND 
    FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("EQUIPMENT_CLASS_ID",OracleType.VarChar,20,entity.EQUIPMENT_CLASS_ID),            
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,20,entity.PROCESS_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }

        #endregion



    }
}
