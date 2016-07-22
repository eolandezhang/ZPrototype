using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using DBUtility;
using Model.Settings;

namespace DAL.Settings
{
    public class Menu
    {
        public List<MenuEntity> GetData()
        {

            return OracleHelper.SelectedToIList<MenuEntity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.ID,
       A.PID,
       A.NAME,
       A.TITLE,
       A.URL,
       A.OPEN,
       A.TARGET,
       A.ICON,
       A.SORT,
       B.NAME PNAME
  FROM MENU A LEFT JOIN MENU B
  ON A.PID=B.ID
 ORDER BY A.SORT
", null);
        }
        public List<MenuEntity> GetDataNoRoot()
        {

            return OracleHelper.SelectedToIList<MenuEntity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT A.ID,
       A.PID,
       A.NAME,
       A.TITLE,
       A.URL,
       A.OPEN,
       A.TARGET,
       A.ICON,
       A.SORT,
       B.NAME PNAME
  FROM MENU A , MENU B
  WHERE A.PID=B.ID
 ORDER BY A.SORT
", null);
        }

        public int Add(MenuEntity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM MENU
 WHERE NAME = :NAME 
AND PID=:PID
",
                new[]{
                    OracleHelper.MakeInParam("NAME",OracleType.NVarChar,30,entity.NAME),
                    OracleHelper.MakeInParam("PID",OracleType.Number,entity.PID),
                })
                ) > 0;
            if (exist)
            {
                return 0;
            }
            #endregion

            decimal id = Convert.ToDecimal(OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"SELECT SEQ_MENU.NEXTVAL FROM DUAL",
                null
                ));

            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
INSERT INTO MENU (ID,
                  PID,
                  NAME,
                  TITLE,
                  URL,
                  OPEN,
                  TARGET,
                  ICON,
                  SORT)
     VALUES (:ID,
             :PID,
             :NAME,
             :TITLE,
             :URL,
             :OPEN,
             :TARGET,
             :ICON,
             :SORT)
", new[]{
     OracleHelper.MakeInParam("ID",OracleType.Number,id),
     OracleHelper.MakeInParam("PID",OracleType.Number,entity.PID),
     OracleHelper.MakeInParam("NAME",OracleType.NVarChar,30,string.IsNullOrEmpty(entity.NAME)?"":entity.NAME),
     OracleHelper.MakeInParam("TITLE",OracleType.NVarChar,30,string.IsNullOrEmpty(entity.TITLE)?"":entity.TITLE),
     OracleHelper.MakeInParam("URL",OracleType.NVarChar,100,string.IsNullOrEmpty(entity.URL)?"":entity.URL),
     OracleHelper.MakeInParam("OPEN",OracleType.NVarChar,10,string.IsNullOrEmpty(entity.OPEN)?"true":entity.OPEN),
     OracleHelper.MakeInParam("TARGET",OracleType.NVarChar,10,string.IsNullOrEmpty(entity.TARGET)?"_self":entity.TARGET),
     OracleHelper.MakeInParam("ICON",OracleType.NVarChar,100,string.IsNullOrEmpty(entity.ICON)?"":entity.ICON),
     OracleHelper.MakeInParam("SORT",OracleType.Number,entity.SORT)
 });
        }

        public int Edit(MenuEntity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM MENU
 WHERE NAME = :NAME
AND PID=:PID
AND ID!=:ID
",
                new[]{
                    OracleHelper.MakeInParam("ID",OracleType.Number,entity.ID),
                    OracleHelper.MakeInParam("PID",OracleType.Number,entity.PID),
                    OracleHelper.MakeInParam("NAME",OracleType.NVarChar,30,entity.NAME)
                })
                ) > 0;
            if (exist)
            {
                return 0;
            }
            #endregion



            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE MENU
   SET PID = :PID,
       NAME = :NAME,
       TITLE = :TITLE,
       URL = :URL,
       OPEN = :OPEN,
       TARGET = :TARGET,
       ICON = :ICON,
       SORT = :SORT
 WHERE ID = :ID
", new[]{
     OracleHelper.MakeInParam("ID",OracleType.Number,entity.ID),
     OracleHelper.MakeInParam("PID",OracleType.Number,entity.PID),
     OracleHelper.MakeInParam("NAME",OracleType.NVarChar,30,string.IsNullOrEmpty(entity.NAME)?"":entity.NAME),
     OracleHelper.MakeInParam("TITLE",OracleType.NVarChar,30,string.IsNullOrEmpty(entity.TITLE)?"":entity.TITLE),
     OracleHelper.MakeInParam("URL",OracleType.NVarChar,100,string.IsNullOrEmpty(entity.URL)?"":entity.URL),
     OracleHelper.MakeInParam("OPEN",OracleType.NVarChar,10,string.IsNullOrEmpty(entity.OPEN)?"true":entity.OPEN),
     OracleHelper.MakeInParam("TARGET",OracleType.NVarChar,10,string.IsNullOrEmpty(entity.TARGET)?"_self":entity.TARGET),
     OracleHelper.MakeInParam("ICON",OracleType.NVarChar,100,string.IsNullOrEmpty(entity.ICON)?"":entity.ICON),
     OracleHelper.MakeInParam("SORT",OracleType.Number,entity.SORT)
 });
        }

        public int Delete(decimal ID)
        {
            return OracleHelper.ExecuteNonQuery(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"DELETE FROM MENU WHERE ID=:ID",
                new[]{
                OracleHelper.MakeInParam("ID",OracleType.Number,ID)
                });
        }
    }
}
