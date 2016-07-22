using DBUtility;
using Model.BaseInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.BaseInfo
{
    public class RECIPE_LIST
    {
        #region 查询

        public List<RECIPE_LIST_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<RECIPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT
        RECIPE_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        RECIPE_TYPE_ID,
        VALID_FLAG,
        RECIPE_NAME,
        RECIPE_DESC,
        SOLID_CONTENT,
        SCP_VAR,
        BASE_RECIPE,
        STAGE,
        IS_HIGH_VISCOSITY,
        PROC_CONDITION,
        OTHER_CONDITION
FROM RECIPE_LIST
", null);
        }

        public List<RECIPE_LIST_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<RECIPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text,
            @"
SELECT  TOTAL, 
        RECIPE_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        UPDATE_DATE,
        RECIPE_TYPE_ID,
        VALID_FLAG,
        RECIPE_NAME,
        RECIPE_DESC,
        SOLID_CONTENT,
        SCP_VAR,
        BASE_RECIPE,
        STAGE,
        IS_HIGH_VISCOSITY,
        PROC_CONDITION,
        OTHER_CONDITION
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            RECIPE_ID,
            FACTORY_ID,
            PRODUCT_TYPE_ID,
            PRODUCT_PROC_TYPE_ID,
            UPDATE_USER,
            UPDATE_DATE,
            RECIPE_TYPE_ID,
            VALID_FLAG,
            RECIPE_NAME,
            RECIPE_DESC,
            SOLID_CONTENT,
            SCP_VAR,
            BASE_RECIPE,
            STAGE,
            IS_HIGH_VISCOSITY,
            PROC_CONDITION,
            OTHER_CONDITION
        FROM (  SELECT 
                    RECIPE_ID,
                    FACTORY_ID,
                    PRODUCT_TYPE_ID,
                    PRODUCT_PROC_TYPE_ID,
                    UPDATE_USER,
                    TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                    RECIPE_TYPE_ID,
                    VALID_FLAG,
                    RECIPE_NAME,
                    RECIPE_DESC,
                    SOLID_CONTENT,
                    SCP_VAR,
                    BASE_RECIPE,
                    STAGE,
                    IS_HIGH_VISCOSITY,
                    PROC_CONDITION,
                    OTHER_CONDITION
                FROM RECIPE_LIST ) T1,          
            (  SELECT COUNT (1) TOTAL FROM RECIPE_LIST ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
  ", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<RECIPE_LIST_Entity> GetDataById(string RECIPE_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<RECIPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        RECIPE_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        RECIPE_TYPE_ID,
        VALID_FLAG,
        RECIPE_NAME,
        RECIPE_DESC,
        SOLID_CONTENT,
        SCP_VAR,
        BASE_RECIPE,
        STAGE,
        IS_HIGH_VISCOSITY,
        PROC_CONDITION,
        OTHER_CONDITION
FROM RECIPE_LIST
WHERE RECIPE_ID=:RECIPE_ID 
    AND FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
" + queryStr, new[]{
            OracleHelper.MakeInParam("RECIPE_ID",OracleType.VarChar,15,RECIPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID)
            });
        }

        public List<RECIPE_LIST_Entity> GetDataByTypeId(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string RECIPE_TYPE_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<RECIPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        RECIPE_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        RECIPE_TYPE_ID,
        VALID_FLAG,
        RECIPE_NAME,
        RECIPE_DESC,
        SOLID_CONTENT,
        SCP_VAR,
        BASE_RECIPE,
        STAGE,
        IS_HIGH_VISCOSITY,
        PROC_CONDITION,
        OTHER_CONDITION
FROM RECIPE_LIST
WHERE  FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
    AND RECIPE_TYPE_ID=:RECIPE_TYPE_ID
" + queryStr, new[]{            
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("RECIPE_TYPE_ID",OracleType.VarChar,15,RECIPE_TYPE_ID)
            });
        }
        public List<RECIPE_LIST_Entity> GetDataQuery(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string RECIPE_TYPE_ID, string RECIPE_ID, string RECIPE_NAME, string RECIPE_DESC, string queryStr)
        {
            string str1 = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            
            List<OracleParameter> paramList = new List<OracleParameter>{  
              OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
              OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
              OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
              OracleHelper.MakeInParam("RECIPE_TYPE_ID", OracleType.VarChar, 15, RECIPE_TYPE_ID.ToUpper())           
            };
                        
            if (string.IsNullOrEmpty(RECIPE_ID))
            {
                str1 = "";
            }
            else
            {
                str1 = " AND UPPER(RECIPE_ID) LIKE '%'||:RECIPE_ID||'%' ";
                paramList.Add(OracleHelper.MakeInParam("RECIPE_ID", OracleType.VarChar, RECIPE_ID.Trim().ToUpper()));
            }

            if (string.IsNullOrEmpty(RECIPE_NAME))
            {
                str2 = "";
            }
            else
            {
                str2 = " AND UPPER(RECIPE_NAME) LIKE '%'||:RECIPE_NAME||'%' ";
                paramList.Add(OracleHelper.MakeInParam("RECIPE_NAME", OracleType.VarChar, RECIPE_NAME.Trim().ToUpper()));
            }

            if (string.IsNullOrEmpty(RECIPE_DESC))
            {
                str3 = "";
            }
            else
            {
                str3 = " AND UPPER(RECIPE_DESC) LIKE '%'||:RECIPE_DESC||'%'  ";
                paramList.Add(OracleHelper.MakeInParam("RECIPE_DESC", OracleType.VarChar, RECIPE_DESC.Trim().ToUpper()));
            }
            OracleParameter[] param = param = paramList.ToArray();
            string str = str1 + str2 + str3;

            return OracleHelper.SelectedToIList<RECIPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        RECIPE_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        RECIPE_TYPE_ID,
        VALID_FLAG,
        RECIPE_NAME,
        RECIPE_DESC,
        SOLID_CONTENT,
        SCP_VAR,
        BASE_RECIPE,
        STAGE,
        IS_HIGH_VISCOSITY,
        PROC_CONDITION,
        OTHER_CONDITION
FROM RECIPE_LIST
WHERE  FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID     
    AND UPPER(RECIPE_TYPE_ID) =:RECIPE_TYPE_ID 
" + str + queryStr, param);
        }

        public List<RECIPE_LIST_Entity> GetDataByTypeAndId(string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID, string RECIPE_TYPE_ID, string RECIPE_ID)
        { 
            return OracleHelper.SelectedToIList<RECIPE_LIST_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        RECIPE_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        RECIPE_TYPE_ID,
        VALID_FLAG,
        RECIPE_NAME,
        RECIPE_DESC,
        SOLID_CONTENT,
        SCP_VAR,
        BASE_RECIPE,
        STAGE,
        IS_HIGH_VISCOSITY,
        PROC_CONDITION,
        OTHER_CONDITION
FROM RECIPE_LIST
WHERE  FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
    AND RECIPE_TYPE_ID=:RECIPE_TYPE_ID
    AND RECIPE_ID =:RECIPE_ID
", new[]{
      OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
      OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
      OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID),
      OracleHelper.MakeInParam("RECIPE_TYPE_ID", OracleType.VarChar, 15, RECIPE_TYPE_ID),
      OracleHelper.MakeInParam("RECIPE_ID", OracleType.VarChar,15, RECIPE_ID)
  });
        }

        public bool GetDataValidateId(string RECIPE_ID, string FACTORY_ID, string PRODUCT_TYPE_ID, string PRODUCT_PROC_TYPE_ID)
        {
            return 1 == Convert.ToInt32(OracleHelper.ExecuteScalar(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  COUNT(1)
FROM RECIPE_LIST
WHERE RECIPE_ID=:RECIPE_ID 
    AND FACTORY_ID=:FACTORY_ID 
    AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID 
    AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("RECIPE_ID",OracleType.VarChar,15,RECIPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,PRODUCT_PROC_TYPE_ID)
            }));
        }

        #endregion

        #region 新增

        public int PostAdd(RECIPE_LIST_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM RECIPE_LIST
 WHERE RECIPE_ID=:RECIPE_ID AND FACTORY_ID=:FACTORY_ID AND PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
",
                new[]{
                    OracleHelper.MakeInParam("RECIPE_ID",OracleType.VarChar,15,entity.RECIPE_ID),
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
INSERT INTO RECIPE_LIST (
        RECIPE_ID,
        FACTORY_ID,
        PRODUCT_TYPE_ID,
        PRODUCT_PROC_TYPE_ID,
        UPDATE_USER,
        UPDATE_DATE,
        RECIPE_TYPE_ID,
        VALID_FLAG,
        RECIPE_NAME,
        RECIPE_DESC,
        SOLID_CONTENT,
        SCP_VAR,
        BASE_RECIPE,
        STAGE,
        IS_HIGH_VISCOSITY,
        PROC_CONDITION,
        OTHER_CONDITION
)
VALUES (
        :RECIPE_ID,
        :FACTORY_ID,
        :PRODUCT_TYPE_ID,
        :PRODUCT_PROC_TYPE_ID,
        :UPDATE_USER,
        :UPDATE_DATE,
        :RECIPE_TYPE_ID,
        :VALID_FLAG,
        :RECIPE_NAME,
        :RECIPE_DESC,
        :SOLID_CONTENT,
        :SCP_VAR,
        :BASE_RECIPE,
        :STAGE,
        :IS_HIGH_VISCOSITY,
        :PROC_CONDITION,
        :OTHER_CONDITION
)
", new[]{
            OracleHelper.MakeInParam("RECIPE_ID",OracleType.VarChar,15,entity.RECIPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("RECIPE_TYPE_ID",OracleType.VarChar,15,entity.RECIPE_TYPE_ID),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("RECIPE_NAME",OracleType.VarChar,15,entity.RECIPE_NAME),
            OracleHelper.MakeInParam("RECIPE_DESC",OracleType.VarChar,50,entity.RECIPE_DESC),
            OracleHelper.MakeInParam("SOLID_CONTENT",OracleType.Number,0,entity.SOLID_CONTENT),
            OracleHelper.MakeInParam("SCP_VAR",OracleType.VarChar,5,entity.SCP_VAR),
            OracleHelper.MakeInParam("BASE_RECIPE",OracleType.VarChar,1,entity.BASE_RECIPE),
            OracleHelper.MakeInParam("STAGE",OracleType.VarChar,3,entity.STAGE),
            OracleHelper.MakeInParam("IS_HIGH_VISCOSITY",OracleType.VarChar,1,entity.IS_HIGH_VISCOSITY),
            OracleHelper.MakeInParam("PROC_CONDITION",OracleType.VarChar,200,entity.PROC_CONDITION),
            OracleHelper.MakeInParam("OTHER_CONDITION",OracleType.VarChar,50,entity.OTHER_CONDITION)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(RECIPE_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE RECIPE_LIST SET 
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    RECIPE_TYPE_ID=:RECIPE_TYPE_ID,
    VALID_FLAG=:VALID_FLAG,
    RECIPE_NAME=:RECIPE_NAME,
    RECIPE_DESC=:RECIPE_DESC,
    SOLID_CONTENT=:SOLID_CONTENT,
    SCP_VAR=:SCP_VAR,
    BASE_RECIPE=:BASE_RECIPE,
    STAGE=:STAGE,
    IS_HIGH_VISCOSITY=:IS_HIGH_VISCOSITY,
    PROC_CONDITION=:PROC_CONDITION,
    OTHER_CONDITION=:OTHER_CONDITION
 WHERE 
        RECIPE_ID=:RECIPE_ID AND  
        FACTORY_ID=:FACTORY_ID AND  
        PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND  
        PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID 
", new[]{
            OracleHelper.MakeInParam("RECIPE_ID",OracleType.VarChar,15,entity.RECIPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("RECIPE_TYPE_ID",OracleType.VarChar,15,entity.RECIPE_TYPE_ID),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("RECIPE_NAME",OracleType.VarChar,15,entity.RECIPE_NAME),
            OracleHelper.MakeInParam("RECIPE_DESC",OracleType.VarChar,50,entity.RECIPE_DESC),
            OracleHelper.MakeInParam("SOLID_CONTENT",OracleType.Number,0,entity.SOLID_CONTENT),
            OracleHelper.MakeInParam("SCP_VAR",OracleType.VarChar,5,entity.SCP_VAR),
            OracleHelper.MakeInParam("BASE_RECIPE",OracleType.VarChar,1,entity.BASE_RECIPE),
            OracleHelper.MakeInParam("STAGE",OracleType.VarChar,3,entity.STAGE),
            OracleHelper.MakeInParam("IS_HIGH_VISCOSITY",OracleType.VarChar,1,entity.IS_HIGH_VISCOSITY),
            OracleHelper.MakeInParam("PROC_CONDITION",OracleType.VarChar,200,entity.PROC_CONDITION),
            OracleHelper.MakeInParam("OTHER_CONDITION",OracleType.VarChar,50,entity.OTHER_CONDITION)
            });
        }

        #endregion

        #region 删除
        public int PostDelete(RECIPE_LIST_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM RECIPE_LIST 
WHERE
    RECIPE_ID=:RECIPE_ID AND 
    FACTORY_ID=:FACTORY_ID AND 
    PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID AND 
    PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
", new[]{
            OracleHelper.MakeInParam("RECIPE_ID",OracleType.VarChar,15,entity.RECIPE_ID),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_TYPE_ID),
            OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,15,entity.PRODUCT_PROC_TYPE_ID)
            });
        }

        #endregion



    }
}
