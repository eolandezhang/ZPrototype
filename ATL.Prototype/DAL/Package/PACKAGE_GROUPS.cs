using DBUtility;
using Model.Package;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace DAL.Package
{
    public class PACKAGE_GROUPS
    {

        #region 查询

        public List<PACKAGE_GROUPS_Entity> GetData(string factoryId, string packageNo, string versionNo, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_GROUPS_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        FACTORY_ID,
        VERSION_NO,
        GROUP_NO,
        GROUP_QTY,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE
FROM PACKAGE_GROUPS
 WHERE     PACKAGE_NO = :PACKAGE_NO
       AND VERSION_NO = :VERSION_NO
       AND FACTORY_ID = :FACTORY_ID
" + queryStr + " ORDER BY GROUP_NO", new[]{     
     OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,packageNo),
     OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,versionNo),
     OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,factoryId)
 });
        }

        public List<PACKAGE_GROUPS_Entity> GetGroupsNotInDesignInfo(string factoryId, string packageNo, string versionNo)
        {
            var designInfo = new DAL.Package.PACKAGE_DESIGN_INFO().GetData(factoryId, packageNo, versionNo, "");
            string groupNos = "";
            foreach (var d in designInfo)
            {
                groupNos += "'" + d.GROUP_NO + "'" + ",";
            }
            groupNos = groupNos.TrimEnd(',');
            List<PACKAGE_GROUPS_Entity> groups = new List<PACKAGE_GROUPS_Entity>();
            if (!string.IsNullOrEmpty(groupNos))
            {
                groups = GetData(factoryId, packageNo, versionNo, " AND GROUP_NO NOT IN (" + groupNos + ")");

            }
            groups = GetData(factoryId, packageNo, versionNo, "");
            return groups;

        }
        #endregion

        #region 新增

        public int PostAdd(PACKAGE_GROUPS_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PACKAGE_GROUPS
 WHERE PACKAGE_NO=:PACKAGE_NO AND FACTORY_ID=:FACTORY_ID AND VERSION_NO=:VERSION_NO AND GROUP_NO=:GROUP_NO
",
                new[]{
                    OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
                    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
                    OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
                    OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO)
                })
                ) > 0;
            if (exist)
            {
                return 0;
            }
            #endregion
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
INSERT INTO PACKAGE_GROUPS (
        PACKAGE_NO,
        FACTORY_ID,
        VERSION_NO,
        GROUP_NO,
        GROUP_QTY,
        UPDATE_USER,
        UPDATE_DATE
)
VALUES (
        :PACKAGE_NO,
        :FACTORY_ID,
        :VERSION_NO,
        :GROUP_NO,
        :GROUP_QTY,
        :UPDATE_USER,
        :UPDATE_DATE
)
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO.ToUpper()),
            OracleHelper.MakeInParam("GROUP_QTY",OracleType.Number,0,entity.GROUP_QTY),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture))
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_GROUPS_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PACKAGE_GROUPS SET 
    GROUP_QTY=:GROUP_QTY,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE
 WHERE PACKAGE_NO=:PACKAGE_NO AND FACTORY_ID=:FACTORY_ID AND VERSION_NO=:VERSION_NO AND GROUP_NO=:GROUP_NO
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO.ToUpper()),
            OracleHelper.MakeInParam("GROUP_QTY",OracleType.Number,0,entity.GROUP_QTY),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture))
            });
        }

        #endregion

        #region 删除
        //请移除多余的 "AND" 和 逗号
        public int PostDelete(PACKAGE_GROUPS_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_GROUPS WHERE PACKAGE_NO=:PACKAGE_NO AND FACTORY_ID=:FACTORY_ID AND VERSION_NO=:VERSION_NO AND GROUP_NO=:GROUP_NO
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,0,entity.VERSION_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO)
            });
        }
        public int DeleteByPackageId(PACKAGE_GROUPS_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_GROUPS 
WHERE PACKAGE_NO=:PACKAGE_NO 
    AND FACTORY_ID=:FACTORY_ID 
    AND VERSION_NO=:VERSION_NO     
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,0,entity.VERSION_NO)            
            });
        }
        public int DeleteByPackageIdAndGroupNo(PACKAGE_GROUPS_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_GROUPS 
WHERE PACKAGE_NO=:PACKAGE_NO 
    AND FACTORY_ID=:FACTORY_ID 
    AND VERSION_NO=:VERSION_NO 
    AND GROUP_NO=:GROUP_NO
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,0,entity.VERSION_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO)
            });
        }
        #endregion



    }
}
