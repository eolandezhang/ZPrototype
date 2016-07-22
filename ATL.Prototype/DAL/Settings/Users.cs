using DBUtility;
using Model.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Settings
{
    public class Users
    {
        public List<UsersEntity> GetData()
        {
            return OracleHelper.SelectedToIList<UsersEntity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT USERNAME,
       DEPARTMENT,
       TITLE,
       DESCRIPTION,
       MAIL,
       CREATEDBY,
       TO_CHAR (CREATEDAT, 'MM/DD/YYYY HH24:MI:SS') CREATEDAT,
       MODIFIEDBY,
       TO_CHAR (MODIFIEDAT, 'MM/DD/YYYY HH24:MI:SS') MODIFIEDAT,
       CNNAME,
       FACTORY_ID
  FROM USERS
", null);
        }

        public List<UsersEntity> GetData(decimal pageSize, decimal pageNumber)
        {            
            return OracleHelper.SelectedToIList<UsersEntity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  TOTAL, 
        USERNAME,
        PASSWORD,
        DEPARTMENT,
        TITLE,
        DESCRIPTION,
        MAIL,
        CREATEDBY,
        CREATEDAT,
        MODIFIEDBY,
        MODIFIEDAT,
        CNNAME,
        FACTORY_ID
FROM (SELECT
            T3.TOTAL TOTAL,
            ROWNUM AS ROWINDEX,
            USERNAME,
            PASSWORD,
            DEPARTMENT,
            TITLE,
            DESCRIPTION,
            MAIL,
            CREATEDBY,
            CREATEDAT,
            MODIFIEDBY,
            MODIFIEDAT,
            CNNAME,
            FACTORY_ID
        FROM (  SELECT 
                    USERNAME,
                    PASSWORD,
                    DEPARTMENT,
                    TITLE,
                    DESCRIPTION,
                    MAIL,
                    CREATEDBY,
                    TO_CHAR (CREATEDAT, 'MM/DD/YYYY HH24:MI:SS') CREATEDAT,
                    MODIFIEDBY,
                    TO_CHAR (MODIFIEDAT, 'MM/DD/YYYY HH24:MI:SS') MODIFIEDAT,
                    CNNAME,
                    FACTORY_ID
                FROM USERS ) T1,          
            (  SELECT COUNT (1) TOTAL FROM USERS ) T3
    WHERE ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER) T2
WHERE T2.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber)
 });
        }
        public List<UsersEntity> GetDataByUserNum(string DESCRIPTION, string queryStr)
        {
            return OracleHelper.SelectedToIList<UsersEntity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        USERNAME,
        PASSWORD,
        DEPARTMENT,
        TITLE,
        DESCRIPTION,
        MAIL,
        CREATEDBY,
        TO_CHAR (CREATEDAT, 'MM/DD/YYYY HH24:MI:SS') CREATEDAT,
        MODIFIEDBY,
        TO_CHAR (MODIFIEDAT, 'MM/DD/YYYY HH24:MI:SS') MODIFIEDAT,
        CNNAME,
        FACTORY_ID
FROM USERS
WHERE DESCRIPTION=:DESCRIPTION 
" + queryStr, new[]{
            OracleHelper.MakeInParam("DESCRIPTION",OracleType.VarChar,30,DESCRIPTION)
            });
        }

        public int Add(UsersEntity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM USERS
 WHERE USERNAME = :USERNAME
",
                new[]{
                    OracleHelper.MakeInParam("USERNAME",OracleType.NVarChar,30,entity.USERNAME)
                })
                ) > 0;
            if (exist)
            {
                return 0;
            }
            #endregion

            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
INSERT INTO USERS (USERNAME,                   
                   DEPARTMENT,
                   TITLE,
                   DESCRIPTION,
                   MAIL,
                   CREATEDBY,
                   CREATEDAT,
                   CNNAME)
     VALUES (:USERNAME,            
             :DEPARTMENT,
             :TITLE,
             :DESCRIPTION,
             :MAIL,
             :CREATEDBY,
             SYSDATE,
             :CNNAME)
", new[]{
     OracleHelper.MakeInParam("USERNAME",OracleType.NVarChar,30,entity.USERNAME),
     OracleHelper.MakeInParam("DEPARTMENT",OracleType.NVarChar,30,entity.DEPARTMENT),
     OracleHelper.MakeInParam("TITLE",OracleType.NVarChar,30,string.IsNullOrEmpty(entity.TITLE)?"":entity.TITLE),
     OracleHelper.MakeInParam("DESCRIPTION",OracleType.NVarChar,100,string.IsNullOrEmpty(entity.DESCRIPTION)?"":entity.DESCRIPTION),
     OracleHelper.MakeInParam("MAIL",OracleType.NVarChar,30,string.IsNullOrEmpty(entity.MAIL)?"":entity.MAIL),
     OracleHelper.MakeInParam("CREATEDBY",OracleType.NVarChar,30,string.IsNullOrEmpty(entity.CREATEDBY)?"":entity.CREATEDBY),
     OracleHelper.MakeInParam("CNNAME",OracleType.NVarChar,30,string.IsNullOrEmpty(entity.CNNAME)?"":entity.CNNAME)
 });
        }

        public int Edit(UsersEntity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE USERS
   SET DEPARTMENT = :DEPARTMENT,
       TITLE = :TITLE,
       DESCRIPTION = :DESCRIPTION,
       MAIL = :MAIL,
       MODIFIEDBY = :MODIFIEDBY,
       MODIFIEDAT = SYSDATE,
       CNNAME = :CNNAME
 WHERE USERNAME = :USERNAME
", new[]{
     OracleHelper.MakeInParam("USERNAME",OracleType.NVarChar,30,entity.USERNAME),
     OracleHelper.MakeInParam("DEPARTMENT",OracleType.NVarChar,30,entity.DEPARTMENT),
     OracleHelper.MakeInParam("TITLE",OracleType.NVarChar,30,string.IsNullOrEmpty(entity.TITLE)?"":entity.TITLE),
     OracleHelper.MakeInParam("DESCRIPTION",OracleType.NVarChar,100,string.IsNullOrEmpty(entity.DESCRIPTION)?"":entity.DESCRIPTION),
     OracleHelper.MakeInParam("MAIL",OracleType.NVarChar,30,string.IsNullOrEmpty(entity.MAIL)?"":entity.MAIL),
     OracleHelper.MakeInParam("MODIFIEDBY",OracleType.NVarChar,30,string.IsNullOrEmpty(entity.MODIFIEDBY)?"":entity.MODIFIEDBY),
     OracleHelper.MakeInParam("CNNAME",OracleType.NVarChar,30,string.IsNullOrEmpty(entity.CNNAME)?"":entity.CNNAME)
 });
        }

        public int Edit_factory_id(UsersEntity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE USERS
   SET 
       FACTORY_ID=:FACTORY_ID
 WHERE USERNAME = :USERNAME
", new[]{
     OracleHelper.MakeInParam("USERNAME",OracleType.NVarChar,30,entity.USERNAME),     
     OracleHelper.MakeInParam("FACTORY_ID",OracleType.NVarChar,5,string.IsNullOrEmpty(entity.FACTORY_ID)?"":entity.FACTORY_ID)
 });
        }

        public int Delete(string userName)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text, @"DELETE FROM USERS WHERE USERNAME=:USERNAME", new[]{
            OracleHelper.MakeInParam("USERNAME",OracleType.NVarChar,30,userName)
            });
        }
    }
}
