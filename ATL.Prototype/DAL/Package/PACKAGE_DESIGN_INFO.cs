using DBUtility;
using Model.Package;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;

namespace DAL.Package
{
    public class PACKAGE_DESIGN_INFO
    {

        #region 查询

        public List<PACKAGE_DESIGN_INFO_Entity> GetData()
        {
            return OracleHelper.SelectedToIList<PACKAGE_DESIGN_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        FACTORY_ID,
        CELL_CAP,
        BEG_VOL,
        END_VOL,
        ANODE_STUFF_ID,
        ANODE_FORMULA_ID,
        ANODE_COATING_WEIGHT,
        ANODE_DENSITY,
        ANODE_FOIL_ID,
        CATHODE_STUFF_ID,
        CATHODE_FORMULA_ID,
        CATHODE_COATING_WEIGHT,
        CATHODE_DENSITY,
        CATHODE_FOIL_ID,
        SEPARATOR_ID,
        ELECTROLYTE_ID,
        INJECTION_QTY,
        LIQUID_PER,
        MODEL_DESC,
        VALID_FLAG,
        TO_CHAR (DESIGN_DATE, 'MM/DD/YYYY') DESIGN_DATE,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        ANODE_THICKNESS,
        CATHODE_THICKNESS
FROM PACKAGE_DESIGN_INFO
", null);
        }

        public List<PACKAGE_DESIGN_INFO_Entity> GetData(decimal pageSize, decimal pageNumber, string factoryId, string packageNo, string versionNo, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_DESIGN_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT  B.TOTAL,
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        FACTORY_ID,
        CELL_CAP,
        BEG_VOL,
        END_VOL,
        ANODE_STUFF_ID,
        ANODE_FORMULA_ID,
        ANODE_COATING_WEIGHT,
        ANODE_DENSITY,
        ANODE_FOIL_ID,
        CATHODE_STUFF_ID,
        CATHODE_FORMULA_ID,
        CATHODE_COATING_WEIGHT,
        CATHODE_DENSITY,
        CATHODE_FOIL_ID,
        SEPARATOR_ID,
        ELECTROLYTE_ID,
        INJECTION_QTY,
        LIQUID_PER,
        MODEL_DESC,
        VALID_FLAG,
        DESIGN_DATE,
        UPDATE_USER,
        UPDATE_DATE,
        ANODE_THICKNESS,
        CATHODE_THICKNESS
FROM (  SELECT ROWNUM AS ROWINDEX,
                PACKAGE_NO,
                GROUP_NO,
                VERSION_NO,
                FACTORY_ID,
                CELL_CAP,
                BEG_VOL,
                END_VOL,
                ANODE_STUFF_ID,
                ANODE_FORMULA_ID,
                ANODE_COATING_WEIGHT,
                ANODE_DENSITY,
                ANODE_FOIL_ID,
                CATHODE_STUFF_ID,
                CATHODE_FORMULA_ID,
                CATHODE_COATING_WEIGHT,
                CATHODE_DENSITY,
                CATHODE_FOIL_ID,
                SEPARATOR_ID,
                ELECTROLYTE_ID,
                INJECTION_QTY,
                LIQUID_PER,
                MODEL_DESC,
                VALID_FLAG,
                TO_CHAR (DESIGN_DATE, 'MM/DD/YYYY') DESIGN_DATE,
                UPDATE_USER,
                TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
                ANODE_THICKNESS,
                CATHODE_THICKNESS
        FROM PACKAGE_DESIGN_INFO
           WHERE 
                PACKAGE_NO=:PACKAGE_NO 
                AND VERSION_NO=:VERSION_NO 
                AND FACTORY_ID=:FACTORY_ID  "
                + queryStr +
               @" AND ROWNUM <= :PAGE_SIZE * :PAGE_NUMBER ) A,
     (  SELECT COUNT (1) TOTAL FROM PACKAGE_DESIGN_INFO ) B
WHERE A.ROWINDEX > (:PAGE_NUMBER - 1) * :PAGE_SIZE
", new[]{
     OracleHelper.MakeInParam("PAGE_SIZE",OracleType.Number,pageSize),
     OracleHelper.MakeInParam("PAGE_NUMBER",OracleType.Number,pageNumber),
     OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,packageNo),
     OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,versionNo),
     OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,factoryId)
 });
        }
        public List<PACKAGE_DESIGN_INFO_Entity> GetDataById(string groupNo, string factoryId, string packageNo, string versionNo)
        {
            return OracleHelper.SelectedToIList<PACKAGE_DESIGN_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT PACKAGE_NO,
       GROUP_NO,
       VERSION_NO,
       FACTORY_ID,
       CELL_CAP,
       BEG_VOL,
       END_VOL,
       ANODE_STUFF_ID,
       ANODE_FORMULA_ID,
       ANODE_COATING_WEIGHT,
       ANODE_DENSITY,
       ANODE_FOIL_ID,
       CATHODE_STUFF_ID,
       CATHODE_FORMULA_ID,
       CATHODE_COATING_WEIGHT,
       CATHODE_DENSITY,
       CATHODE_FOIL_ID,
       SEPARATOR_ID,
       ELECTROLYTE_ID,
       INJECTION_QTY,
       LIQUID_PER,
       MODEL_DESC,
       VALID_FLAG,
       TO_CHAR (DESIGN_DATE, 'MM/DD/YYYY') DESIGN_DATE,
       UPDATE_USER,
       TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       ANODE_THICKNESS,
       CATHODE_THICKNESS
  FROM PACKAGE_DESIGN_INFO
 WHERE     PACKAGE_NO = :PACKAGE_NO
       AND GROUP_NO = :GROUP_NO
       AND VERSION_NO = :VERSION_NO
       AND FACTORY_ID = :FACTORY_ID
       AND VALID_FLAG=1
", new[]{
    OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,packageNo),
    OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,groupNo),
    OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,versionNo),
    OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,factoryId)
 });
        }

        // 所有组别的设计信息        
        public List<PACKAGE_DESIGN_INFO_Entity> GetData(string factoryId, string packageNo, string versionNo, string queryStr)
        {
            return OracleHelper.SelectedToIList<PACKAGE_DESIGN_INFO_Entity>(PubConstant.ConnectionString, CommandType.Text, @"
SELECT
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        FACTORY_ID,
        CELL_CAP,
        BEG_VOL,
        END_VOL,
        ANODE_STUFF_ID,
        ANODE_FORMULA_ID,
        ANODE_COATING_WEIGHT,
        ANODE_DENSITY,
        ANODE_FOIL_ID,
        CATHODE_STUFF_ID,
        CATHODE_FORMULA_ID,
        CATHODE_COATING_WEIGHT,
        CATHODE_DENSITY,
        CATHODE_FOIL_ID,
        SEPARATOR_ID,
        ELECTROLYTE_ID,
        INJECTION_QTY,
        LIQUID_PER,
        MODEL_DESC,
        VALID_FLAG,
        TO_CHAR (DESIGN_DATE, 'MM/DD/YYYY') DESIGN_DATE,
        UPDATE_USER,
        TO_CHAR (UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
        ANODE_THICKNESS,
        CATHODE_THICKNESS
FROM PACKAGE_DESIGN_INFO
   WHERE 
                PACKAGE_NO=:PACKAGE_NO 
                AND VERSION_NO=:VERSION_NO 
                AND FACTORY_ID=:FACTORY_ID  
" + queryStr, new[]{     
     OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,packageNo),
     OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,versionNo),
     OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,factoryId)
 });
        }

        public List<PACKAGE_DESIGN_INFO_Entity> PostDataQuery(PACKAGE_DESIGN_INFO_Entity entity)
        {
            List<OracleParameter> paramList = new List<OracleParameter>{             
              OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),              
              OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
              OracleHelper.MakeInParam("PRODUCT_TYPE_ID",OracleType.VarChar,20,entity.PRODUCT_TYPE_ID),
              OracleHelper.MakeInParam("PRODUCT_PROC_TYPE_ID",OracleType.VarChar,20,entity.PRODUCT_PROC_TYPE_ID)             
            };
            StringBuilder sb = new StringBuilder();
            #region 电池容量
            if (entity.check_CELL_CAP && entity.CELL_CAP != 0)
            {
                if (entity.CELL_CAP_tolerance != 0)
                {
                    sb.Append(" AND CELL_CAP BETWEEN :CELL_CAP-:CELL_CAP_tolerance AND :CELL_CAP+:CELL_CAP_tolerance ");
                    paramList.Add(OracleHelper.MakeInParam("CELL_CAP", OracleType.Number, entity.CELL_CAP));
                    paramList.Add(OracleHelper.MakeInParam("CELL_CAP_tolerance", OracleType.Number, entity.CELL_CAP_tolerance));
                }
                else
                {
                    sb.Append(" AND CELL_CAP =:CELL_CAP ");
                    paramList.Add(OracleHelper.MakeInParam("CELL_CAP", OracleType.Number, entity.CELL_CAP));
                }
            }
            #endregion

            #region 起始电压
            if (entity.check_BEG_VOL && entity.BEG_VOL != 0)
            {
                if (entity.BEG_VOL_tolerance != 0)
                {
                    sb.Append(" AND BEG_VOL BETWEEN :BEG_VOL-:BEG_VOL_tolerance AND :BEG_VOL+:BEG_VOL_tolerance ");
                    paramList.Add(OracleHelper.MakeInParam("BEG_VOL", OracleType.Number, entity.BEG_VOL));
                    paramList.Add(OracleHelper.MakeInParam("BEG_VOL_tolerance", OracleType.Number, entity.BEG_VOL_tolerance));
                }
                else
                {
                    sb.Append(" AND BEG_VOL =:BEG_VOL ");
                    paramList.Add(OracleHelper.MakeInParam("BEG_VOL", OracleType.Number, entity.BEG_VOL));
                }
            }
            #endregion


            #region 截至电压
            if (entity.check_END_VOL && entity.END_VOL != 0)
            {
                if (entity.END_VOL_tolerance != 0)
                {
                    sb.Append(" AND END_VOL BETWEEN :END_VOL-:END_VOL_tolerance AND :END_VOL+:END_VOL_tolerance ");
                    paramList.Add(OracleHelper.MakeInParam("END_VOL", OracleType.Number, entity.END_VOL));
                    paramList.Add(OracleHelper.MakeInParam("END_VOL_tolerance", OracleType.Number, entity.END_VOL_tolerance));
                }
                else
                {
                    sb.Append(" AND END_VOL =:END_VOL ");
                    paramList.Add(OracleHelper.MakeInParam("END_VOL", OracleType.Number, entity.END_VOL));
                }
            }
            #endregion

            #region 阳极涂布重量
            if (entity.check_ANODE_COATING_WEIGHT && entity.ANODE_COATING_WEIGHT != 0)
            {
                if (entity.ANODE_COATING_WEIGHT_tolerance != 0)
                {
                    sb.Append(" AND ANODE_COATING_WEIGHT BETWEEN :ANODE_COATING_WEIGHT-:ANODE_COATING_WEIGHT_tolerance AND :ANODE_COATING_WEIGHT+:ANODE_COATING_WEIGHT_tolerance ");
                    paramList.Add(OracleHelper.MakeInParam("ANODE_COATING_WEIGHT", OracleType.Number, entity.ANODE_COATING_WEIGHT));
                    paramList.Add(OracleHelper.MakeInParam("ANODE_COATING_WEIGHT_tolerance", OracleType.Number, entity.ANODE_COATING_WEIGHT_tolerance));
                }
                else
                {
                    sb.Append(" AND ANODE_COATING_WEIGHT =:ANODE_COATING_WEIGHT ");
                    paramList.Add(OracleHelper.MakeInParam("ANODE_COATING_WEIGHT", OracleType.Number, entity.ANODE_COATING_WEIGHT));
                }
            }
            #endregion

            #region 阳极压实密度
            if (entity.check_ANODE_DENSITY && entity.ANODE_DENSITY != 0)
            {
                if (entity.ANODE_DENSITY_tolerance != 0)
                {
                    sb.Append(" AND ANODE_DENSITY BETWEEN :ANODE_DENSITY-:ANODE_DENSITY_tolerance AND :ANODE_DENSITY+:ANODE_DENSITY_tolerance ");
                    paramList.Add(OracleHelper.MakeInParam("ANODE_DENSITY", OracleType.Number, entity.ANODE_DENSITY));
                    paramList.Add(OracleHelper.MakeInParam("ANODE_DENSITY_tolerance", OracleType.Number, entity.ANODE_DENSITY_tolerance));
                }
                else
                {
                    sb.Append(" AND ANODE_DENSITY =:ANODE_DENSITY ");
                    paramList.Add(OracleHelper.MakeInParam("ANODE_DENSITY", OracleType.Number, entity.ANODE_DENSITY));
                }
            }
            #endregion

            #region 阴极涂布重量
            if (entity.check_CATHODE_COATING_WEIGHT && entity.CATHODE_COATING_WEIGHT != 0)
            {
                if (entity.CATHODE_COATING_WEIGHT_tolerance != 0)
                {
                    sb.Append(" AND CATHODE_COATING_WEIGHT BETWEEN :CATHODE_COATING_WEIGHT-:CATHODE_COATING_WEIGHT_t AND :CATHODE_COATING_WEIGHT+:CATHODE_COATING_WEIGHT_t ");
                    paramList.Add(OracleHelper.MakeInParam("CATHODE_COATING_WEIGHT", OracleType.Number, entity.CATHODE_COATING_WEIGHT));
                    paramList.Add(OracleHelper.MakeInParam("CATHODE_COATING_WEIGHT_t", OracleType.Number, entity.CATHODE_COATING_WEIGHT_tolerance));
                }
                else
                {
                    sb.Append(" AND CATHODE_COATING_WEIGHT =:CATHODE_COATING_WEIGHT ");
                    paramList.Add(OracleHelper.MakeInParam("CATHODE_COATING_WEIGHT", OracleType.Number, entity.CATHODE_COATING_WEIGHT));
                }
            }
            #endregion

            #region 阴极压实密度
            if (entity.check_CATHODE_DENSITY && entity.CATHODE_DENSITY != 0)
            {
                if (entity.CATHODE_DENSITY_tolerance != 0)
                {
                    sb.Append(" AND CATHODE_DENSITY BETWEEN :CATHODE_DENSITY-:CATHODE_DENSITY_tolerance AND :CATHODE_DENSITY+:CATHODE_DENSITY_tolerance ");
                    paramList.Add(OracleHelper.MakeInParam("CATHODE_DENSITY", OracleType.Number, entity.CATHODE_DENSITY));
                    paramList.Add(OracleHelper.MakeInParam("CATHODE_DENSITY_tolerance", OracleType.Number, entity.CATHODE_DENSITY_tolerance));
                }
                else
                {
                    sb.Append(" AND CATHODE_DENSITY =:CATHODE_DENSITY ");
                    paramList.Add(OracleHelper.MakeInParam("CATHODE_DENSITY", OracleType.Number, entity.CATHODE_DENSITY));
                }
            }
            #endregion

            #region 注液量
            if (entity.check_INJECTION_QTY && entity.INJECTION_QTY != 0)
            {
                if (entity.INJECTION_QTY_tolerance != 0)
                {
                    sb.Append(" AND INJECTION_QTY BETWEEN :INJECTION_QTY-:INJECTION_QTY_tolerance AND :INJECTION_QTY+:INJECTION_QTY_tolerance ");
                    paramList.Add(OracleHelper.MakeInParam("INJECTION_QTY", OracleType.Number, entity.INJECTION_QTY));
                    paramList.Add(OracleHelper.MakeInParam("INJECTION_QTY_tolerance", OracleType.Number, entity.INJECTION_QTY_tolerance));
                }
                else
                {
                    sb.Append(" AND INJECTION_QTY =:INJECTION_QTY ");
                    paramList.Add(OracleHelper.MakeInParam("INJECTION_QTY", OracleType.Number, entity.INJECTION_QTY));
                }
            }
            #endregion

            #region 保液系数
            if (entity.check_LIQUID_PER && entity.LIQUID_PER != 0)
            {
                if (entity.LIQUID_PER_tolerance != 0)
                {
                    sb.Append(" AND LIQUID_PER BETWEEN :LIQUID_PER-:LIQUID_PER_tolerance AND :LIQUID_PER+:LIQUID_PER_tolerance ");
                    paramList.Add(OracleHelper.MakeInParam("LIQUID_PER", OracleType.Number, entity.LIQUID_PER));
                    paramList.Add(OracleHelper.MakeInParam("LIQUID_PER_tolerance", OracleType.Number, entity.LIQUID_PER_tolerance));
                }
                else
                {
                    sb.Append(" AND LIQUID_PER =:LIQUID_PER ");
                    paramList.Add(OracleHelper.MakeInParam("LIQUID_PER", OracleType.Number, entity.LIQUID_PER));
                }
            }
            #endregion

            #region 阳极集流体厚度
            if (entity.check_ANODE_THICKNESS && entity.ANODE_THICKNESS != 0)
            {
                if (entity.ANODE_THICKNESS_tolerance != 0)
                {
                    sb.Append(" AND ANODE_THICKNESS BETWEEN :ANODE_THICKNESS-:ANODE_THICKNESS_tolerance AND :ANODE_THICKNESS+:ANODE_THICKNESS_tolerance ");
                    paramList.Add(OracleHelper.MakeInParam("ANODE_THICKNESS", OracleType.Number, entity.ANODE_THICKNESS));
                    paramList.Add(OracleHelper.MakeInParam("ANODE_THICKNESS_tolerance", OracleType.Number, entity.ANODE_THICKNESS_tolerance));
                }
                else
                {
                    sb.Append(" AND ANODE_THICKNESS =:ANODE_THICKNESS ");
                    paramList.Add(OracleHelper.MakeInParam("ANODE_THICKNESS", OracleType.Number, entity.ANODE_THICKNESS));
                }
            }
            #endregion

            #region 阴极集流体厚度
            if (entity.check_CATHODE_THICKNESS && entity.CATHODE_THICKNESS != 0)
            {
                if (entity.CATHODE_THICKNESS_tolerance != 0)
                {
                    sb.Append(" AND CATHODE_THICKNESS BETWEEN :CATHODE_THICKNESS-:CATHODE_THICKNESS_tolerance AND :CATHODE_THICKNESS+:CATHODE_THICKNESS_tolerance ");
                    paramList.Add(OracleHelper.MakeInParam("CATHODE_THICKNESS", OracleType.Number, entity.CATHODE_THICKNESS));
                    paramList.Add(OracleHelper.MakeInParam("CATHODE_THICKNESS_tolerance", OracleType.Number, entity.CATHODE_THICKNESS_tolerance));
                }
                else
                {
                    sb.Append(" AND CATHODE_THICKNESS =:CATHODE_THICKNESS ");
                    paramList.Add(OracleHelper.MakeInParam("CATHODE_THICKNESS", OracleType.Number, entity.CATHODE_THICKNESS));
                }
            }
            #endregion



            #region 阳极材料
            if (entity.check_ANODE_STUFF_ID && !string.IsNullOrEmpty(entity.ANODE_STUFF_ID))
            {
                sb.Append(" AND ANODE_STUFF_ID =:ANODE_STUFF_ID ");
                paramList.Add(OracleHelper.MakeInParam("ANODE_STUFF_ID", OracleType.VarChar, 20, entity.ANODE_STUFF_ID));
            }
            #endregion

            #region 阳极配方
            if (entity.check_ANODE_FORMULA_ID && !string.IsNullOrEmpty(entity.ANODE_FORMULA_ID))
            {
                sb.Append(" AND ANODE_FORMULA_ID =:ANODE_FORMULA_ID ");
                paramList.Add(OracleHelper.MakeInParam("ANODE_FORMULA_ID", OracleType.VarChar, 20, entity.ANODE_FORMULA_ID));
            }
            #endregion

            #region 阳极集流体材料
            if (entity.check_ANODE_FOIL_ID && !string.IsNullOrEmpty(entity.ANODE_FOIL_ID))
            {
                sb.Append(" AND ANODE_FOIL_ID =:ANODE_FOIL_ID ");
                paramList.Add(OracleHelper.MakeInParam("ANODE_FOIL_ID", OracleType.VarChar, 20, entity.ANODE_FOIL_ID));
            }
            #endregion



            #region 阴极材料
            if (entity.check_CATHODE_STUFF_ID && !string.IsNullOrEmpty(entity.CATHODE_STUFF_ID))
            {
                sb.Append(" AND CATHODE_STUFF_ID =:CATHODE_STUFF_ID ");
                paramList.Add(OracleHelper.MakeInParam("CATHODE_STUFF_ID", OracleType.VarChar, 20, entity.CATHODE_STUFF_ID));
            }
            #endregion

            #region 阴极配方
            if (entity.check_CATHODE_FORMULA_ID && !string.IsNullOrEmpty(entity.CATHODE_FORMULA_ID))
            {
                sb.Append(" AND CATHODE_FORMULA_ID =:CATHODE_FORMULA_ID ");
                paramList.Add(OracleHelper.MakeInParam("CATHODE_FORMULA_ID", OracleType.VarChar, 20, entity.CATHODE_FORMULA_ID));
            }
            #endregion

            #region 阴极集流体材料
            if (entity.check_CATHODE_FOIL_ID && !string.IsNullOrEmpty(entity.CATHODE_FOIL_ID))
            {
                sb.Append(" AND CATHODE_FOIL_ID =:CATHODE_FOIL_ID ");
                paramList.Add(OracleHelper.MakeInParam("CATHODE_FOIL_ID", OracleType.VarChar, 20, entity.CATHODE_FOIL_ID));
            }
            #endregion



            #region 隔离膜材料
            if (entity.check_SEPARATOR_ID && !string.IsNullOrEmpty(entity.SEPARATOR_ID))
            {
                sb.Append(" AND SEPARATOR_ID =:SEPARATOR_ID ");
                paramList.Add(OracleHelper.MakeInParam("SEPARATOR_ID", OracleType.VarChar, 20, entity.SEPARATOR_ID));
            }
            #endregion

            #region 电解液配方
            if (entity.check_ELECTROLYTE_ID && !string.IsNullOrEmpty(entity.ELECTROLYTE_ID))
            {
                sb.Append(" AND ELECTROLYTE_ID =:ELECTROLYTE_ID ");
                paramList.Add(OracleHelper.MakeInParam("ELECTROLYTE_ID", OracleType.VarChar, 20, entity.ELECTROLYTE_ID));
            }
            #endregion
//            try
//            {


//                OracleHelper.ExecuteScalar(
//                   PubConstant.ConnectionString,
//                   CommandType.Text,
//                   @"
//SELECT A.PACKAGE_NO,
//       A.GROUP_NO,
//       A.VERSION_NO,
//       A.FACTORY_ID,
//       A.CELL_CAP,
//       A.BEG_VOL,
//       A.END_VOL,
//       A.ANODE_STUFF_ID,
//       A.ANODE_FORMULA_ID,
//       A.ANODE_COATING_WEIGHT,
//       A.ANODE_DENSITY,
//       A.ANODE_FOIL_ID,
//       A.CATHODE_STUFF_ID,
//       A.CATHODE_FORMULA_ID,
//       A.CATHODE_COATING_WEIGHT,
//       A.CATHODE_DENSITY,
//       A.CATHODE_FOIL_ID,
//       A.SEPARATOR_ID,
//       A.ELECTROLYTE_ID,
//       A.INJECTION_QTY,
//       A.LIQUID_PER,
//       A.MODEL_DESC,
//       A.VALID_FLAG,
//       TO_CHAR (A.DESIGN_DATE, 'MM/DD/YYYY') DESIGN_DATE,
//       A.UPDATE_USER,
//       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
//       A.ANODE_THICKNESS,
//       A.CATHODE_THICKNESS
//  FROM PACKAGE_DESIGN_INFO A,PACKAGE_BASE_INFO B
// WHERE A.PACKAGE_NO=B.PACKAGE_NO
// AND A.VERSION_NO=B.VERSION_NO
// AND A.FACTORY_ID=B.FACTORY_ID
// AND B.PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID
// AND B.PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
// AND B.DELETE_FLAG='0'
// AND B.VALID_FLAG='1'
// AND B.STATUS='5'
// AND B.PACKAGE_NO!=:PACKAGE_NO
// AND B.FACTORY_ID=:FACTORY_ID 
//" + sb.ToString(),
//                   paramList.ToArray()
//                   );
//            }
//            catch (Exception ex)
//            {

//                throw;
//            }
//            return null;

            return OracleHelper.SelectedToIList<PACKAGE_DESIGN_INFO_Entity>(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT A.PACKAGE_NO,
       A.GROUP_NO,
       A.VERSION_NO,
       A.FACTORY_ID,
       A.CELL_CAP,
       A.BEG_VOL,
       A.END_VOL,
       A.ANODE_STUFF_ID,
       A.ANODE_FORMULA_ID,
       A.ANODE_COATING_WEIGHT,
       A.ANODE_DENSITY,
       A.ANODE_FOIL_ID,
       A.CATHODE_STUFF_ID,
       A.CATHODE_FORMULA_ID,
       A.CATHODE_COATING_WEIGHT,
       A.CATHODE_DENSITY,
       A.CATHODE_FOIL_ID,
       A.SEPARATOR_ID,
       A.ELECTROLYTE_ID,
       A.INJECTION_QTY,
       A.LIQUID_PER,
       A.MODEL_DESC,
       A.VALID_FLAG,
       TO_CHAR (A.DESIGN_DATE, 'MM/DD/YYYY') DESIGN_DATE,
       A.UPDATE_USER,
       TO_CHAR (A.UPDATE_DATE, 'MM/DD/YYYY HH24:MI:SS') UPDATE_DATE,
       A.ANODE_THICKNESS,
       A.CATHODE_THICKNESS
  FROM PACKAGE_DESIGN_INFO A,PACKAGE_BASE_INFO B
 WHERE A.PACKAGE_NO=B.PACKAGE_NO
 AND A.VERSION_NO=B.VERSION_NO
 AND A.FACTORY_ID=B.FACTORY_ID
 AND B.PRODUCT_TYPE_ID=:PRODUCT_TYPE_ID
 AND B.PRODUCT_PROC_TYPE_ID=:PRODUCT_PROC_TYPE_ID
 AND B.DELETE_FLAG='0'
 AND B.VALID_FLAG='1'
 AND B.STATUS='5'
 AND B.PACKAGE_NO!=:PACKAGE_NO
 AND B.FACTORY_ID=:FACTORY_ID 
" + sb.ToString(),
                paramList.ToArray()
                );
        }
        #endregion

        #region 新增

        public int PostAdd(PACKAGE_DESIGN_INFO_Entity entity)
        {
            #region 检查重复
            bool exist = Convert.ToDecimal(
                OracleHelper.ExecuteScalar(
                PubConstant.ConnectionString,
                CommandType.Text,
                @"
SELECT COUNT (1)
  FROM PACKAGE_DESIGN_INFO
 WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND VERSION_NO=:VERSION_NO AND FACTORY_ID=:FACTORY_ID
",
                new[]{
                    OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
                    OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
                    OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
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
INSERT INTO PACKAGE_DESIGN_INFO (
        PACKAGE_NO,
        GROUP_NO,
        VERSION_NO,
        FACTORY_ID,
        CELL_CAP,
        BEG_VOL,
        END_VOL,
        ANODE_STUFF_ID,
        ANODE_FORMULA_ID,
        ANODE_COATING_WEIGHT,
        ANODE_DENSITY,
        ANODE_FOIL_ID,
        CATHODE_STUFF_ID,
        CATHODE_FORMULA_ID,
        CATHODE_COATING_WEIGHT,
        CATHODE_DENSITY,
        CATHODE_FOIL_ID,
        SEPARATOR_ID,
        ELECTROLYTE_ID,
        INJECTION_QTY,
        LIQUID_PER,
        MODEL_DESC,
        VALID_FLAG,
        DESIGN_DATE,
        UPDATE_USER,
        UPDATE_DATE,
        ANODE_THICKNESS,
        CATHODE_THICKNESS
)
VALUES (
        :PACKAGE_NO,
        :GROUP_NO,
        :VERSION_NO,
        :FACTORY_ID,
        :CELL_CAP,
        :BEG_VOL,
        :END_VOL,
        :ANODE_STUFF_ID,
        :ANODE_FORMULA_ID,
        :ANODE_COATING_WEIGHT,
        :ANODE_DENSITY,
        :ANODE_FOIL_ID,
        :CATHODE_STUFF_ID,
        :CATHODE_FORMULA_ID,
        :CATHODE_COATING_WEIGHT,
        :CATHODE_DENSITY,
        :CATHODE_FOIL_ID,
        :SEPARATOR_ID,
        :ELECTROLYTE_ID,
        :INJECTION_QTY,
        :LIQUID_PER,
        :MODEL_DESC,
        :VALID_FLAG,
        :DESIGN_DATE,
        :UPDATE_USER,
        :UPDATE_DATE,
        :ANODE_THICKNESS,
        :CATHODE_THICKNESS
)
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("CELL_CAP",OracleType.Number,0,entity.CELL_CAP),
            OracleHelper.MakeInParam("BEG_VOL",OracleType.Number,0,entity.BEG_VOL),
            OracleHelper.MakeInParam("END_VOL",OracleType.Number,0,entity.END_VOL),
            OracleHelper.MakeInParam("ANODE_STUFF_ID",OracleType.VarChar,20,entity.ANODE_STUFF_ID),
            OracleHelper.MakeInParam("ANODE_FORMULA_ID",OracleType.VarChar,20,entity.ANODE_FORMULA_ID),
            OracleHelper.MakeInParam("ANODE_COATING_WEIGHT",OracleType.Number,0,entity.ANODE_COATING_WEIGHT),
            OracleHelper.MakeInParam("ANODE_DENSITY",OracleType.Number,0,entity.ANODE_DENSITY),
            OracleHelper.MakeInParam("ANODE_FOIL_ID",OracleType.VarChar,20,entity.ANODE_FOIL_ID),
            OracleHelper.MakeInParam("CATHODE_STUFF_ID",OracleType.VarChar,20,entity.CATHODE_STUFF_ID),
            OracleHelper.MakeInParam("CATHODE_FORMULA_ID",OracleType.VarChar,20,entity.CATHODE_FORMULA_ID),
            OracleHelper.MakeInParam("CATHODE_COATING_WEIGHT",OracleType.Number,0,entity.CATHODE_COATING_WEIGHT),
            OracleHelper.MakeInParam("CATHODE_DENSITY",OracleType.Number,0,entity.CATHODE_DENSITY),
            OracleHelper.MakeInParam("CATHODE_FOIL_ID",OracleType.VarChar,20,entity.CATHODE_FOIL_ID),
            OracleHelper.MakeInParam("SEPARATOR_ID",OracleType.VarChar,20,entity.SEPARATOR_ID),
            OracleHelper.MakeInParam("ELECTROLYTE_ID",OracleType.VarChar,20,entity.ELECTROLYTE_ID),
            OracleHelper.MakeInParam("INJECTION_QTY",OracleType.Number,0,entity.INJECTION_QTY),
            OracleHelper.MakeInParam("LIQUID_PER",OracleType.Number,0,entity.LIQUID_PER),
            OracleHelper.MakeInParam("MODEL_DESC",OracleType.VarChar,25,entity.MODEL_DESC),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("DESIGN_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.DESIGN_DATE)?DateTime.Now: DateTime.ParseExact(entity.DESIGN_DATE,"MM/dd/yyyy",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("ANODE_THICKNESS",OracleType.Number,0,entity.ANODE_THICKNESS),
            OracleHelper.MakeInParam("CATHODE_THICKNESS",OracleType.Number,0,entity.CATHODE_THICKNESS)
        });
        }

        #endregion

        #region 修改

        public int PostEdit(PACKAGE_DESIGN_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
                @"
UPDATE PACKAGE_DESIGN_INFO SET 
    CELL_CAP=:CELL_CAP,
    BEG_VOL=:BEG_VOL,
    END_VOL=:END_VOL,
    ANODE_STUFF_ID=:ANODE_STUFF_ID,
    ANODE_FORMULA_ID=:ANODE_FORMULA_ID,
    ANODE_COATING_WEIGHT=:ANODE_COATING_WEIGHT,
    ANODE_DENSITY=:ANODE_DENSITY,
    ANODE_FOIL_ID=:ANODE_FOIL_ID,
    CATHODE_STUFF_ID=:CATHODE_STUFF_ID,
    CATHODE_FORMULA_ID=:CATHODE_FORMULA_ID,
    CATHODE_COATING_WEIGHT=:CATHODE_COATING_WEIGHT,
    CATHODE_DENSITY=:CATHODE_DENSITY,
    CATHODE_FOIL_ID=:CATHODE_FOIL_ID,
    SEPARATOR_ID=:SEPARATOR_ID,
    ELECTROLYTE_ID=:ELECTROLYTE_ID,
    INJECTION_QTY=:INJECTION_QTY,
    LIQUID_PER=:LIQUID_PER,
    MODEL_DESC=:MODEL_DESC,
    VALID_FLAG=:VALID_FLAG,
    DESIGN_DATE=:DESIGN_DATE,
    UPDATE_USER=:UPDATE_USER,
    UPDATE_DATE=:UPDATE_DATE,
    ANODE_THICKNESS=:ANODE_THICKNESS,
    CATHODE_THICKNESS=:CATHODE_THICKNESS
 WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND VERSION_NO=:VERSION_NO AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID),
            OracleHelper.MakeInParam("CELL_CAP",OracleType.Number,0,entity.CELL_CAP),
            OracleHelper.MakeInParam("BEG_VOL",OracleType.Number,0,entity.BEG_VOL),
            OracleHelper.MakeInParam("END_VOL",OracleType.Number,0,entity.END_VOL),
            OracleHelper.MakeInParam("ANODE_STUFF_ID",OracleType.VarChar,20,entity.ANODE_STUFF_ID),
            OracleHelper.MakeInParam("ANODE_FORMULA_ID",OracleType.VarChar,20,entity.ANODE_FORMULA_ID),
            OracleHelper.MakeInParam("ANODE_COATING_WEIGHT",OracleType.Number,0,entity.ANODE_COATING_WEIGHT),
            OracleHelper.MakeInParam("ANODE_DENSITY",OracleType.Number,0,entity.ANODE_DENSITY),
            OracleHelper.MakeInParam("ANODE_FOIL_ID",OracleType.VarChar,20,entity.ANODE_FOIL_ID),
            OracleHelper.MakeInParam("CATHODE_STUFF_ID",OracleType.VarChar,20,entity.CATHODE_STUFF_ID),
            OracleHelper.MakeInParam("CATHODE_FORMULA_ID",OracleType.VarChar,20,entity.CATHODE_FORMULA_ID),
            OracleHelper.MakeInParam("CATHODE_COATING_WEIGHT",OracleType.Number,0,entity.CATHODE_COATING_WEIGHT),
            OracleHelper.MakeInParam("CATHODE_DENSITY",OracleType.Number,0,entity.CATHODE_DENSITY),
            OracleHelper.MakeInParam("CATHODE_FOIL_ID",OracleType.VarChar,20,entity.CATHODE_FOIL_ID),
            OracleHelper.MakeInParam("SEPARATOR_ID",OracleType.VarChar,20,entity.SEPARATOR_ID),
            OracleHelper.MakeInParam("ELECTROLYTE_ID",OracleType.VarChar,20,entity.ELECTROLYTE_ID),
            OracleHelper.MakeInParam("INJECTION_QTY",OracleType.Number,0,entity.INJECTION_QTY),
            OracleHelper.MakeInParam("LIQUID_PER",OracleType.Number,0,entity.LIQUID_PER),
            OracleHelper.MakeInParam("MODEL_DESC",OracleType.VarChar,25,entity.MODEL_DESC),
            OracleHelper.MakeInParam("VALID_FLAG",OracleType.VarChar,1,entity.VALID_FLAG),
            OracleHelper.MakeInParam("DESIGN_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.DESIGN_DATE)?DateTime.Now: DateTime.ParseExact(entity.DESIGN_DATE,"MM/dd/yyyy",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("UPDATE_USER",OracleType.VarChar,10,entity.UPDATE_USER),
            OracleHelper.MakeInParam("UPDATE_DATE",OracleType.DateTime,0,string.IsNullOrEmpty(entity.UPDATE_DATE)?DateTime.Now: DateTime.ParseExact(entity.UPDATE_DATE,"MM/dd/yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture)),
            OracleHelper.MakeInParam("ANODE_THICKNESS",OracleType.Number,0,entity.ANODE_THICKNESS),
            OracleHelper.MakeInParam("CATHODE_THICKNESS",OracleType.Number,0,entity.CATHODE_THICKNESS)
            });
        }

        #endregion

        #region 删除

        public int PostDelete(PACKAGE_DESIGN_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_DESIGN_INFO WHERE PACKAGE_NO=:PACKAGE_NO AND GROUP_NO=:GROUP_NO AND VERSION_NO=:VERSION_NO AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }

        public int DeleteByPackageId(PACKAGE_DESIGN_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_DESIGN_INFO 
    WHERE PACKAGE_NO=:PACKAGE_NO         
        AND VERSION_NO=:VERSION_NO 
        AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO),            
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }
        public int DeleteByPackageIdAndGroupNo(PACKAGE_DESIGN_INFO_Entity entity)
        {
            return OracleHelper.ExecuteNonQuery(PubConstant.ConnectionString, CommandType.Text,
@"
DELETE FROM PACKAGE_DESIGN_INFO 
    WHERE PACKAGE_NO=:PACKAGE_NO  
        AND GROUP_NO=:GROUP_NO       
        AND VERSION_NO=:VERSION_NO 
        AND FACTORY_ID=:FACTORY_ID
", new[]{
            OracleHelper.MakeInParam("PACKAGE_NO",OracleType.VarChar,20,entity.PACKAGE_NO), 
            OracleHelper.MakeInParam("GROUP_NO",OracleType.VarChar,5,entity.GROUP_NO),
            OracleHelper.MakeInParam("VERSION_NO",OracleType.VarChar,1,entity.VERSION_NO),
            OracleHelper.MakeInParam("FACTORY_ID",OracleType.VarChar,5,entity.FACTORY_ID)
            });
        }
        #endregion



    }
}
