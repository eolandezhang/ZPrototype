using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class MATERIAL_PN_LIST
    {

        #region 查询

        public List<MATERIAL_PN_LIST_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<MATERIAL_PN_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        MATERIAL_PN_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        MATERIAL_PN_NAME,
        MATERIAL_PN_DESC,
        MATERIAL_TYPE_ID,
        VALID_FLAG
FROM MATERIAL_PN_LIST 
", null);
        }

        public List<MATERIAL_PN_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<MATERIAL_PN_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  TOTAL, 
        MATERIAL_PN_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        UPDATE_DATE,
        MATERIAL_PN_NAME,
        MATERIAL_PN_DESC,
        MATERIAL_TYPE_ID,
        VALID_FLAG
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            MATERIAL_PN_ID,
            FACTORY_ID,
            PRODUCT_TYPE_ID,
            PRODUCT_PROC_TYPE_ID,
            UPDATE_USER,
            UPDATE_DATE,
            MATERIAL_PN_NAME,
            MATERIAL_PN_DESC,
            MATERIAL_TYPE_ID,
            VALID_FLAG
        FROM (  SELECT 
                    MATERIAL_PN_ID,
                    FACTORY_ID,
                    PRODUCT_TYPE_ID,
                    PRODUCT_PROC_TYPE_ID,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    MATERIAL_PN_NAME,
                    MATERIAL_PN_DESC,
                    MATERIAL_TYPE_ID,
                    VALID_FLAG
                FROM MATERIAL_PN_LIST ) T1,          
            (  SELECT COUNT (1) TOTAL FROM MATERIAL_PN_LIST ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<MATERIAL_PN_LIST_Entity> GetDataByCategoryId(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string MATERIAL_CATEGORY_ID)
        {
            return OracleHelper.SelectedToIList<MATERIAL_PN_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        A.MATERIAL_PN_ID,
        A.FACTORY_ID,
        A.PRODUCT_TYPE_ID,
        A.PRODUCT_PROC_TYPE_ID,
        A.UPDATE_USER,
        TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        A.MATERIAL_PN_NAME,
        A.MATERIAL_PN_DESC,
        A.MATERIAL_TYPE_ID,
        A.VALID_FLAG
FROM MATERIAL_PN_LIST A,MATERIAL_TYPE_LIST B,MATERIAL_CATEGORY_LIST C
WHERE A.FACTORY_ID=:FACTORY_ID
    AND A.PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID
    AND A.PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
    AND A.MATERIAL_TYPE_ID=B.MATERIAL_TYPE_ID
    AND B.MATERIAL_CATEGORY_ID=C.MATERIAL_CATEGORY_ID
    AND C.MATERIAL_CATEGORY_ID=:MATERIAL_CATEGORY_ID
    AND A.VALID_FLAG=1
", new[]{
 OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
 OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
 OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
 OracleHelper.MakeInParam("MATERIAL_CATEGORY_ID",OracleType.VarChar,20,MATERIAL_CATEGORY_ID)
 });
        }

        public bool GetDataValidateId(string MATERIAL_PN_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string MATERIAL_CATEGORY_ID)
        {
            var r = OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT(1)
FROM MATERIAL_PN_LIST A,MATERIAL_TYPE_LIST B,MATERIAL_CATEGORY_LIST C
WHERE UPPER(MATERIAL_PN_ID)=:MATERIAL_PN_ID
    AND A.FACTORY_ID=:FACTORY_ID
    AND A.PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID
    AND A.PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
    AND A.MATERIAL_TYPE_ID=B.MATERIAL_TYPE_ID
    AND B.MATERIAL_CATEGORY_ID=C.MATERIAL_CATEGORY_ID
    AND C.MATERIAL_CATEGORY_ID=:MATERIAL_CATEGORY_ID  
", new[]{
     OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,20,MATERIAL_PN_ID.ToUpper()),
 OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
 OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
 OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
 OracleHelper.MakeInParam("MATERIAL_CATEGORY_ID",OracleType.VarChar,20,MATERIAL_CATEGORY_ID)
 });
            int result = Convert.ToInt32(r);
            return 1 == result;
        }

        public List<MATERIAL_PN_LIST_Entity> GetDataById(string MATERIAL_PN_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<MATERIAL_PN_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        MATERIAL_PN_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        MATERIAL_PN_NAME,
        MATERIAL_PN_DESC,
        MATERIAL_TYPE_ID,
        VALID_FLAG
FROM MATERIAL_PN_LIST
WHERE MATERIAL_PN_ID=:MATERIAL_PN_ID 
    AND FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,20,MATERIAL_PN_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID)
            });
        }

        public List<MATERIAL_PN_LIST_Entity> GetDataByType(string FACTORY_ID, string MATERIAL_TYPE_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<MATERIAL_PN_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        MATERIAL_PN_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        MATERIAL_PN_NAME,
        MATERIAL_PN_DESC,
        MATERIAL_TYPE_ID,
        VALID_FLAG
FROM MATERIAL_PN_LIST
WHERE FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
    AND MATERIAL_TYPE_ID=:MATERIAL_TYPE_ID
" + queryStr, new[]{            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("MATERIAL_TYPE_ID",OracleType.VarChar,20,MATERIAL_TYPE_ID)
            });
        }

        public List<MATERIAL_PN_LIST_Entity> GetDataQuery(string MATERIAL_TYPE_GRP_NUM, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string MATERIAL_TYPE_ID, string MATERIAL_PN_ID, string MATERIAL_PN_NAME, string MATERIAL_PN_DESC, string queryStr)
        {

            string str1 = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            List<OracleParameter> paramList = new List<OracleParameter>{             
              OracleHelper.MakeInParam("MATERIAL_TYPE_GRP_NUM",OracleType.VarChar,20,MATERIAL_TYPE_GRP_NUM),
              OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
              OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
              OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID)             
            };

            if (string.IsNullOrEmpty(MATERIAL_TYPE_ID))
            {
                str1 = "";
            }
            else
            {
                str1 = " AND UPPER(C.MATERIAL_TYPE_ID) LIKE '%'||:MATERIAL_TYPE_ID||'%' ";
                paramList.Add(OracleHelper.MakeInParam("MATERIAL_TYPE_ID", OracleType.VarChar, MATERIAL_TYPE_ID.Trim().ToUpper()));
            }
            if (string.IsNullOrEmpty(MATERIAL_PN_ID))
            {
                str2 = "";
            }
            else
            {
                str2 = " AND UPPER(C.MATERIAL_PN_ID) LIKE '%'||:MATERIAL_PN_ID||'%' ";
                paramList.Add(OracleHelper.MakeInParam("MATERIAL_PN_ID", OracleType.VarChar, MATERIAL_PN_ID.Trim().ToUpper()));
            }

            if (string.IsNullOrEmpty(MATERIAL_PN_NAME))
            {
                str3 = "";
            }
            else
            {
                str3 = " AND UPPER(C.MATERIAL_PN_NAME) LIKE '%'||:MATERIAL_PN_NAME||'%' ";
                paramList.Add(OracleHelper.MakeInParam("MATERIAL_PN_NAME", OracleType.VarChar, MATERIAL_PN_NAME.Trim().ToUpper()));
            }

            if (string.IsNullOrEmpty(MATERIAL_PN_DESC))
            {
                str4 = "";
            }
            else
            {
                str4 = " AND UPPER(C.MATERIAL_PN_DESC) LIKE '%'||:MATERIAL_PN_DESC||'%'  ";
                paramList.Add(OracleHelper.MakeInParam("MATERIAL_PN_DESC", OracleType.VarChar, MATERIAL_PN_DESC.Trim().ToUpper()));
            }
            OracleParameter[] param = param = paramList.ToArray();
            string str = str1 + str2 + str3 + str4;
            return OracleHelper.SelectedToIList<MATERIAL_PN_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT C.MATERIAL_PN_ID,
       C.FACTORY_ID,
       C.PRODUCT_TYPE_ID,
       C.PRODUCT_PROC_TYPE_ID,
       C.UPDATE_USER,
       TO_CHAR (C.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       C.MATERIAL_PN_NAME,
       C.MATERIAL_PN_DESC,
       C.MATERIAL_TYPE_ID,
       C.VALID_FLAG
  FROM MATERIAL_TYPE_GRP_LIST A, MATERIAL_TYPE_LIST B, MATERIAL_PN_LIST C
 WHERE     A.MATERIAL_TYPE_GRP_NUM = :MATERIAL_TYPE_GRP_NUM
       AND A.FACTORY_ID = :FACTORY_ID
       AND A.PRODUCT_TYPE_ID = :PRODUCT_TYPE_ID
       AND A.PRODUCT_PROC_TYPE_ID = :PRODUCT_PROC_TYPE_ID
       AND B.MATERIAL_TYPE_ID = A.MATERIAL_TYPE_ID
       AND B.FACTORY_ID = A.FACTORY_ID
       AND B.PRODUCT_TYPE_ID = A.PRODUCT_TYPE_ID
       AND B.PRODUCT_PROC_TYPE_ID = A.PRODUCT_PROC_TYPE_ID
       AND C.FACTORY_ID = A.FACTORY_ID
       AND C.PRODUCT_TYPE_ID = A.PRODUCT_TYPE_ID
       AND C.PRODUCT_PROC_TYPE_ID = A.PRODUCT_PROC_TYPE_ID
       AND C.MATERIAL_TYPE_ID = A.MATERIAL_TYPE_ID
" + str + queryStr, param);
        }

        public List<MATERIAL_PN_LIST_Entity> GetDataByGrpAndID(string MATERIAL_TYPE_GRP_NUM, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string MATERIAL_PN_ID)
        {           
            return OracleHelper.SelectedToIList<MATERIAL_PN_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT C.MATERIAL_PN_ID,
       C.FACTORY_ID,
       C.PRODUCT_TYPE_ID,
       C.PRODUCT_PROC_TYPE_ID,
       C.UPDATE_USER,
       TO_CHAR (C.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       C.MATERIAL_PN_NAME,
       C.MATERIAL_PN_DESC,
       C.MATERIAL_TYPE_ID,
       C.VALID_FLAG
  FROM MATERIAL_TYPE_GRP_LIST A, MATERIAL_TYPE_LIST B, MATERIAL_PN_LIST C
 WHERE     A.MATERIAL_TYPE_GRP_NUM = :MATERIAL_TYPE_GRP_NUM
       AND A.FACTORY_ID = :FACTORY_ID
       AND A.PRODUCT_TYPE_ID = :PRODUCT_TYPE_ID
       AND A.PRODUCT_PROC_TYPE_ID = :PRODUCT_PROC_TYPE_ID
       AND B.MATERIAL_TYPE_ID = A.MATERIAL_TYPE_ID
       AND B.FACTORY_ID = A.FACTORY_ID
       AND B.PRODUCT_TYPE_ID = A.PRODUCT_TYPE_ID
       AND B.PRODUCT_PROC_TYPE_ID = A.PRODUCT_PROC_TYPE_ID
       AND C.FACTORY_ID = A.FACTORY_ID
       AND C.PRODUCT_TYPE_ID = A.PRODUCT_TYPE_ID
       AND C.PRODUCT_PROC_TYPE_ID = A.PRODUCT_PROC_TYPE_ID
       AND C.MATERIAL_TYPE_ID = A.MATERIAL_TYPE_ID
       AND C.MATERIAL_PN_ID = :MATERIAL_PN_ID 
", new[]{
     OracleHelper.MakeInParam("MATERIAL_TYPE_GRP_NUM",OracleType.VarChar,20,MATERIAL_TYPE_GRP_NUM),
     OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
     OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
     OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
     OracleHelper.MakeInParam("MATERIAL_PN_ID", OracleType.VarChar,20, MATERIAL_PN_ID)
 });
        }

        #endregion

        #region 新增

        public int PostAdd(MATERIAL_PN_LIST_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM MATERIAL_PN_LIST
 WHERE MATERIAL_PN_ID=:MATERIAL_PN_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
",
                new[]{
                    OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,20,entity.MATERIAL_PN_ID),
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
INSERT INTO MATERIAL_PN_LIST (
        MATERIAL_PN_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        UPDATE_DATE,
        MATERIAL_PN_NAME,
        MATERIAL_PN_DESC,
        MATERIAL_TYPE_ID,
        VALID_FLAG
)
VALUES (
        :MATERIAL_PN_ID,
        :FACTORY_ID,
        :PRODUCT_TYPE_ID,
        :PRODUCT_PROC_TYPE_ID,
        :UPDATE_USER,
        :UPDATE_DATE,
        :MATERIAL_PN_NAME,
        :MATERIAL_PN_DESC,
        :MATERIAL_TYPE_ID,
        :VALID_FLAG
)
", new[]{
            OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,20,entity.MATERIAL_PN_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("MATERIAL_PN_NAME",OracleType.VarChar,20,entity.MATERIAL_PN_NAME),
            OracleHelper.MakeInParam("MATERIAL_PN_DESC",OracleType.VarChar,20,entity.MATERIAL_PN_DESC),
            OracleHelper.MakeInParam("MATERIAL_TYPE_ID",OracleType.VarChar,20,entity.MATERIAL_TYPE_ID),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(MATERIAL_PN_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE MATERIAL_PN_LIST SET 
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    MATERIAL_PN_NAME=:MATERIAL_PN_NAME,
    MATERIAL_PN_DESC=:MATERIAL_PN_DESC,
    MATERIAL_TYPE_ID=:MATERIAL_TYPE_ID,
    VALID_FLAG=:VALID_FLAG
 WHERE MATERIAL_PN_ID=:MATERIAL_PN_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,20,entity.MATERIAL_PN_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("MATERIAL_PN_NAME",OracleType.VarChar,20,entity.MATERIAL_PN_NAME),
            OracleHelper.MakeInParam("MATERIAL_PN_DESC",OracleType.VarChar,20,entity.MATERIAL_PN_DESC),
            OracleHelper.MakeInParam("MATERIAL_TYPE_ID",OracleType.VarChar,20,entity.MATERIAL_TYPE_ID),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG)
            });
        }

        #endregion

        #region 删除
        //请移除多余的 "AND" 和 逗号
        public int PostDelete(MATERIAL_PN_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM MATERIAL_PN_LIST WHERE MATERIAL_PN_ID=:MATERIAL_PN_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("MATERIAL_PN_ID",OracleType.VarChar,20,entity.MATERIAL_PN_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID)
            });
        }

        #endregion



    }
}
