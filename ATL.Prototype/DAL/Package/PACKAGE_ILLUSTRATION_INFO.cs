using DBUtility;
using Model.Package;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Package
{
    public class PACKAGE_ILLUSTRATION_INFO
    {
        #region 查询

        public List<PACKAGE_ILLUSTRATION_INFO_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<PACKAGE_ILLUSTRATION_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        FACTORY_ID,
        PROCESS_ID,
        ILLUSTRATION_DESC,
        ILLUSTRATION_DATA,
        VALID_FLAG,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        ILLUSTRATION_ID
FROM PACKAGE_ILLUSTRATION_INFO
", null);
        }

        public List<PACKAGE_ILLUSTRATION_INFO_Entity> GetData(decimal pageSize, decimal pageNumber)
        {
            return OracleHelper.SelectedToIList<PACKAGE_ILLUSTRATION_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  B.TOTAL,
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        FACTORY_ID,
        PROCESS_ID,
        ILLUSTRATION_DESC,
        ILLUSTRATION_DATA,
        VALID_FLAG,
        UPDATE_USER,
        UPDATE_DATE,
        ILLUSTRATION_ID
FROM (  SELECT ROWNUM AS ROWINDEX,
                PACKAGE_NO,
                GROUP_NO,
                VERSION_NO,
                FACTORY_ID,
                PROCESS_ID,
                ILLUSTRATION_DESC,
                ILLUSTRATION_DATA,
                VALID_FLAG,
                UPDATE_USER,
                TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                ILLUSTRATION_ID
        FROM PACKAGE_ILLUSTRATION_INFO
           WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER ) A,
     (  SELECT COUNT (1) TOTAL FROM PACKAGE_ILLUSTRATION_INFO ) B
WHERE A.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }

        public List<PACKAGE_ILLUSTRATION_INFO_Entity> GetDataByProcessIdAndGroupNo(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID)
        {
            return OracleHelper.SelectedToIList<PACKAGE_ILLUSTRATION_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        A.PACKAGE_NO,
        A.GROUP_NO,
        A.VERSION_NO,
        A.FACTORY_ID,
        A.PROCESS_ID,
        A.ILLUSTRATION_DESC,
        A.ILLUSTRATION_DATA,
        A.VALID_FLAG,
        A.UPDATE_USER,
        TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        A.ILLUSTRATION_ID,
        B.IMG_LENGTH
FROM PACKAGE_ILLUSTRATION_INFO A,ILLUSTRATION_LIST B
WHERE A.PACKAGE_NO=:PACKAGE_NO 
    AND A.GROUP_NO=:GROUP_NO 
    AND A.VERSION_NO=:VERSION_NO 
    AND A.FACTORY_ID=:FACTORY_ID 
    AND A.PROCESS_ID=:PROCESS_ID
    AND B.ILLUSTRATION_ID=A.ILLUSTRATION_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID)
            });
        }

        public IDataReader GetDataById(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID)
        {
            return OracleHelper.ExecuteReader(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        FACTORY_ID,
        PROCESS_ID,
        ILLUSTRATION_DESC,
        ILLUSTRATION_DATA,
        VALID_FLAG,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        ILLUSTRATION_ID
FROM PACKAGE_ILLUSTRATION_INFO
WHERE PACKAGE_NO=:PACKAGE_NO 
    AND GROUP_NO=:GROUP_NO 
    AND VERSION_NO=:VERSION_NO 
    AND FACTORY_ID=:FACTORY_ID 
    AND PROCESS_ID=:PROCESS_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,PROCESS_ID)
            });
        }
        public byte[] Get_ILLUSTRATION_DATA(string PACKAGE_NO, string GROUP_NO, string VERSION_NO, string FACTORY_ID, string PROCESS_ID)
        {
            byte[] result = null;
            var reader = GetDataById(PACKAGE_NO, GROUP_NO, VERSION_NO, FACTORY_ID, PROCESS_ID);
            if (reader.Read())
            {
                result = (byte[])reader["ILLUSTRATION_DATA"];
            }
            else { return null; }
            return result;
        }

        public List<PACKAGE_ILLUSTRATION_INFO_Entity> GetDataByPackageId(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_ILLUSTRATION_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        FACTORY_ID,
        PROCESS_ID,
        ILLUSTRATION_DESC,
        ILLUSTRATION_DATA,
        VALID_FLAG,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        ILLUSTRATION_ID
FROM PACKAGE_ILLUSTRATION_INFO
WHERE PACKAGE_NO=:PACKAGE_NO     
    AND VERSION_NO=:VERSION_NO 
    AND FACTORY_ID=:FACTORY_ID  
" + queryStr, new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,PACKAGE_NO),           
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,FACTORY_ID)
            });
        }
        #endregion

        #region 新增

        public int PostAdd(PACKAGE_ILLUSTRATION_INFO_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PACKAGE_ILLUSTRATION_INFO
 WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND VERSION_NO=:VERSION_NO AND FACTORY_ID=:FACTORY_ID AND PROCESS_ID=:PROCESS_ID
",
                new[]{
                    OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
                    OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
                    OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
                    OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID)
                })
                ) > 0;
            if (exist)
            {
                return 0;
            }
            #endregion
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
INSERT INTO PACKAGE_ILLUSTRATION_INFO (
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        FACTORY_ID,
        PROCESS_ID,
        ILLUSTRATION_DESC,       
        VALID_FLAG,
        UPDATE_USER,
        UPDATE_DATE,
        ILLUSTRATION_ID
)
VALUES (
        :PACKAGE_NO,
        :GROUP_NO,
        :VERSION_NO,
        :FACTORY_ID,
        :PROCESS_ID,
        :ILLUSTRATION_DESC,        
        :VALID_FLAG,
        :UPDATE_USER,
        :UPDATE_DATE,
        :ILLUSTRATION_ID
)
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("ILLUSTRATION_DESC",OracleType.VarChar,25,entity.ILLUSTRATION_DESC),           
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,25,entity.ILLUSTRATION_ID)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_ILLUSTRATION_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PACKAGE_ILLUSTRATION_INFO SET 
    ILLUSTRATION_DESC=:ILLUSTRATION_DESC,    
    VALID_FLAG=:VALID_FLAG,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    ILLUSTRATION_ID=:ILLUSTRATION_ID
 WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND VERSION_NO=:VERSION_NO AND FACTORY_ID=:FACTORY_ID AND PROCESS_ID=:PROCESS_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
            OracleHelper.MakeInParam("ILLUSTRATION_DESC",OracleType.VarChar,25,entity.ILLUSTRATION_DESC),            
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,25,entity.ILLUSTRATION_ID)
            });
        }
        //新增时,用基础表的图片,更新Package的图片
        public int PostEdit_UpdateByIllustrationId(PACKAGE_ILLUSTRATION_INFO_Entity entity)
        {

            return OracleHelper.ExecuteNonQuery(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
UPDATE PACKAGE_ILLUSTRATION_INFO D
   SET ILLUSTRATION_DATA =
           (SELECT C.ILLUSTRATION_DATA
               FROM PACKAGE_ILLUSTRATION_INFO A,
                    PACKAGE_BASE_INFO B,
                    ILLUSTRATION_LIST C
              WHERE     A.FACTORY_ID = :FACTORY_ID
                    AND A.PACKAGE_NO = :PACKAGE_NO
                    AND A.VERSION_NO = :VERSION_NO
                    AND A.GROUP_NO = :GROUP_NO
                    AND A.ILLUSTRATION_ID = :ILLUSTRATION_ID
                    AND A.PROCESS_ID = :PROCESS_ID
                    AND A.FACTORY_ID = B.FACTORY_ID
                    AND A.PACKAGE_NO = B.PACKAGE_NO
                    AND A.VERSION_NO = B.VERSION_NO
                    AND C.FACTORY_ID = B.FACTORY_ID
                    AND C.PRODUCT_TYPE_ID = B.PRODUCT_TYPE_ID
                    AND C.PRODUCT_PROC_TYPE_ID = B.PRODUCT_PROC_TYPE_ID
                    AND C.ILLUSTRATION_ID = A.ILLUSTRATION_ID) 
                    WHERE D.FACTORY_ID=:FACTORY_ID
                    AND D.PACKAGE_NO=:PACKAGE_NO
                    AND D.VERSION_NO=:VERSION_NO
                    AND D.GROUP_NO=:GROUP_NO
                    AND D.ILLUSTRATION_ID=:ILLUSTRATION_ID
                    AND D.PROCESS_ID=:PROCESS_ID
", new[]{
     OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
     OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
     OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
     OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
     OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
     OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,25,entity.ILLUSTRATION_ID)
                });
        }

        public int PostEdit_UploadImg(PACKAGE_ILLUSTRATION_INFO_Entity entity)
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
UPDATE PACKAGE_ILLUSTRATION_INFO SET
    ILLUSTRATION_DATA=:ILLUSTRATION_DATA
 WHERE PACKAGE_NO=:PACKAGE_NO 
    AND GROUP_NO=:GROUP_NO 
    AND VERSION_NO=:VERSION_NO 
    AND FACTORY_ID=:FACTORY_ID
    AND PROCESS_ID=:PROCESS_ID
    AND ILLUSTRATION_ID=:ILLUSTRATION_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID), 
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID), 
            OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,25,entity.ILLUSTRATION_ID), 
            OracleHelper.MakeInParam("ILLUSTRATION_DATA",OracleType.Blob,bt)
            });
        }
        #endregion

        #region 删除
        public int PostDelete(PACKAGE_ILLUSTRATION_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_ILLUSTRATION_INFO WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND VERSION_NO=:VERSION_NO AND FACTORY_ID=:FACTORY_ID AND PROCESS_ID=:PROCESS_ID AND ILLUSTRATION_ID=:ILLUSTRATION_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID),
             OracleHelper.MakeInParam("ILLUSTRATION_ID",OracleType.VarChar,25,entity.ILLUSTRATION_ID)
            });
        }
        public int DeleteByPackageId(PACKAGE_ILLUSTRATION_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_ILLUSTRATION_INFO 
    WHERE PACKAGE_NO=:PACKAGE_NO         
        AND VERSION_NO=:VERSION_NO 
        AND FACTORY_ID=:FACTORY_ID        
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),            
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)           
            });
        }
        public int DeleteByPackageIdAndGroupNo(PACKAGE_ILLUSTRATION_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_ILLUSTRATION_INFO 
    WHERE PACKAGE_NO=:PACKAGE_NO     
        AND GROUP_NO=:GROUP_NO    
        AND VERSION_NO=:VERSION_NO 
        AND FACTORY_ID=:FACTORY_ID 
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),    
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)           
            });
        }
        public int DeleteByProcessIDAndGroupNo(PACKAGE_ILLUSTRATION_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_ILLUSTRATION_INFO
      WHERE     PACKAGE_NO = :PACKAGE_NO
            AND GROUP_NO = :GROUP_NO
            AND VERSION_NO = :VERSION_NO
            AND FACTORY_ID = :FACTORY_ID
            AND PROCESS_ID = :PROCESS_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,15,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("PROCESS_ID",OracleType.VarChar,15,entity.PROCESS_ID)            
            });
        }
        #endregion



    }
}
